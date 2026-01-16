using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BitbucketSystem.Data;
using BitbucketSystem.Data.Models;

using DbFile = BitbucketSystem.Data.Models.File;

namespace BitbucketSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // знайти нормальний
            Console.OutputEncoding = System.Text.Encoding.UTF8; 
            Console.WriteLine("=== STARTING BITBUCKET SYSTEM (FULL LAB) ===");

            using var context = new BitbucketContext();
            
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            SeedBaseData(context);

            Console.WriteLine("\n=== SECTION 2: DML ===");
            
            InsertSpecificData(context);

            var issuesToClose = context.Issues.Where(i => i.AssigneeId == 6).ToList();
            foreach (var issue in issuesToClose)
            {
                issue.IssueStatus = "closed";
            }
            context.SaveChanges();
            Console.WriteLine($"[Update]]: Closed {issuesToClose.Count} issues for Assignee ID 6");

            var repoToDelete = context.Repositories
                .Include(r => r.Issues)
                .Include(r => r.Commits)
                .FirstOrDefault(r => r.Name == "Softuni-Teamwork");

            if (repoToDelete != null)
            {
                repoToDelete.Contributors.Clear();
                
                context.Commits.RemoveRange(repoToDelete.Commits);
                context.Issues.RemoveRange(repoToDelete.Issues);
                
                context.Repositories.Remove(repoToDelete);
                context.SaveChanges();
                Console.WriteLine($"[Delete]: Repository 'Softuni-Teamwork' deleted.");
            }
            else
            {
                Console.WriteLine($"[Delete]: Repository 'Softuni-Teamwork' not found (create it in pre-seed if needed).");
            }

            Console.WriteLine("\n=== SECTION 3: QUERIES ===");

            var commitsQuery = context.Commits
                .OrderBy(c => c.Id)
                .ThenBy(c => c.Message)
                .ThenBy(c => c.RepositoryId)
                .ThenBy(c => c.ContributorId)
                .Select(c => new { c.Id, c.Message, c.RepositoryId, c.ContributorId })
                .ToList();
            
            Console.WriteLine("\n--- 1. Commits Sorted ---");
            foreach (var c in commitsQuery.Take(5)) Console.WriteLine($"{c.Id} | {c.Message} | Repo:{c.RepositoryId}");

            var frontendFiles = context.Files
                .Where(f => f.Size > 1000 && f.Name.Contains("html"))
                .OrderByDescending(f => f.Size)
                .ThenBy(f => f.Id)
                .ThenBy(f => f.Name)
                .Select(f => new { f.Id, f.Name, f.Size })
                .ToList();

            Console.WriteLine("\n--- 2. Front-end Files ---");
            foreach (var f in frontendFiles) Console.WriteLine($"{f.Name} ({f.Size})");

            var issuesQuery = context.Issues
                .OrderByDescending(i => i.Id)
                .ThenBy(i => i.AssigneeId) 
                .Select(i => new { Username = i.Assignee.Username, Title = i.Title })
                .ToList();

            Console.WriteLine("\n--- 3. Issues Assignment ---");
            foreach (var i in issuesQuery.Take(5)) Console.WriteLine($"{i.Username} : {i.Title}");

            var parentIds = context.Files.Where(f => f.ParentId != null).Select(f => f.ParentId).Distinct().ToList();
            
            var singleFiles = context.Files
                .Where(f => !parentIds.Contains(f.Id)) 
                .OrderBy(f => f.Id)
                .ThenBy(f => f.Name)
                .ThenByDescending(f => f.Size)
                .Select(f => new { f.Id, f.Name, f.Size })
                .ToList();

            Console.WriteLine("\n--- 4. Single Files --");
            foreach (var f in singleFiles.Take(5)) Console.WriteLine($"{f.Id} | {f.Name} | {f.Size} KB");

            var topRepos = context.Repositories
                .Select(r => new { r.Id, r.Name, CommitCount = r.Commits.Count })
                .OrderByDescending(x => x.CommitCount)
                .ThenBy(x => x.Id)
                .ThenBy(x => x.Name)
                .Take(5)
                .ToList();

            Console.WriteLine("\n-- 5. Top Repositories ---");
            foreach (var r in topRepos) Console.WriteLine($"{r.Name} - {r.CommitCount} commits");

            var avgSizeUsers = context.Users
                .Where(u => u.Commits.Any()) 
                .Select(u => new 
                { 
                    u.Username, 
                    AvgSize = u.Commits.SelectMany(c => c.Files).Any() 
                        ? u.Commits.SelectMany(c => c.Files).Average(f => f.Size) 
                        : 0 
                })
                .OrderByDescending(u => u.AvgSize)
                .ThenBy(u => u.Username)
                .ToList();

            Console.WriteLine("\n--- 6. Users Average File Size ---");
            foreach (var u in avgSizeUsers.Take(5)) Console.WriteLine($"{u.Username} - {u.AvgSize:F2}");


            Console.WriteLine("\n=== SECTION 4: PROGRAMMABILTY ===");

            Console.WriteLine("\n---- Function: User Commit Count ---");
            string userNameToCheck = "Bohdan"; 
            int count = context.Commits.Count(c => c.Contributor.Username == userNameToCheck);
            Console.WriteLine($"user '{userNameToCheck}' has {count} commits");

            Console.WriteLine("\n-- Procedure: Search files by extension --");
            string ext = "html"; 
            
            
            var foundFiles = context.Files
                .Where(f => f.Name.EndsWith("." + ext))
                .OrderBy(f => f.Id)
                .ThenBy(f => f.Name)
                .ThenByDescending(f => f.Size)
                .Select(f => new { f.Id, f.Name, f.Size })
                .ToList();

            Console.WriteLine($"Searching for '{ext}':");
            foreach (var f in foundFiles)
            {
                Console.WriteLine($"{f.Id} | {f.Name} | {f.Size} KB");
            }
            
            Console.WriteLine("\n!!! ALL TASKS COMPLETED !!!");
        }

        private static void InsertSpecificData(BitbucketContext context)
        {
            Console.WriteLine("Inserting data (task 2)");

            var filesToAdd = new List<DbFile>
            {
                new DbFile { Name = "Trade.idk", Size = 2598.0m, ParentId = 1, CommitId = 1 },
                new DbFile { Name = "menu.net", Size = 9238.31m, ParentId = 2, CommitId = 2 },
                
                new DbFile { Name = "Administrate.soshy", Size = 1246.93m, ParentId = 3, CommitId = 3 },
                new DbFile { Name = "Controller.php", Size = 7353.15m, ParentId = 4, CommitId = 4 },
                new DbFile { Name = "Find.java", Size = 9957.86m, ParentId = 5, CommitId = 5 },
                new DbFile { Name = "Controller.json", Size = 14034.87m, ParentId = 3, CommitId = 6 },
                new DbFile { Name = "Operate.xix", Size = 7662.92m, ParentId = 7, CommitId = 7 }
            };

            var issuesToAdd = new List<Issue>
            {
                new Issue { Title = "Critical Problem with HomeController.cs file", IssueStatus = "open", RepositoryId = 1, AssigneeId = 4 },
                new Issue { Title = "Typo fix in Judge.html", IssueStatus = "open", RepositoryId = 4, AssigneeId = 3 },
                
                new Issue { Title = "Implement documentation for UsersService.cs", IssueStatus = "closed", RepositoryId = 8, AssigneeId = 2 },
                new Issue { Title = "Unreachable code in Index.cs", IssueStatus = "open", RepositoryId = 9, AssigneeId = 8 }
            };

            if (context.Users.Count() >= 8 && context.Repositories.Count() >= 9)
            {
                context.Files.AddRange(filesToAdd);
                context.Issues.AddRange(issuesToAdd);
                context.SaveChanges();
                Console.WriteLine("Specific Files and Issues added.");
            }
            else
            {
                
                Console.WriteLine("Skipping specific inserts: Not enough base data (Users/Repos) to satisfy Foreign Keys.");
            }
        }

        private static void SeedBaseData(BitbucketContext context)
        {
            Console.WriteLine("Seeding base data (Users, Repos, Commits)...");

            var users = new List<User>();
            for (int i = 1; i <= 10; i++)
            {
                users.Add(new User { Username = $"User_{i}", Email = $"user{i}@test.com", Password = "pass" });
            }
            context.Users.AddRange(users);
            context.SaveChanges();

            var repos = new List<Repository>();
            for (int i = 1; i <= 10; i++)
            {
                repos.Add(new Repository { Name = $"Repo_{i}" });
            }
            repos.Add(new Repository { Name = "Softuni-Teamwork" });
            
            context.Repositories.AddRange(repos);
            context.SaveChanges();

            var commits = new List<Commit>();
            for (int i = 1; i <= 10; i++)
            {
                commits.Add(new Commit 
                { 
                    Message = $"Commit {i}", 
                    IssueDate = DateTime.Now, 
                    RepositoryId = i, 
                    ContributorId = i 
                });
            }
            context.Commits.AddRange(commits);
            context.SaveChanges();
            
            var files = new List<DbFile>();
            for (int i = 1; i <= 10; i++)
            {
                files.Add(new DbFile { Name = $"ParentFile_{i}.txt", Size = 100 + i, CommitId = i });
            }
            files.Add(new DbFile { Name = "Index.html", Size = 1500.50m, CommitId = 1 });
            files.Add(new DbFile { Name = "About.html", Size = 1200.00m, CommitId = 2 });

            context.Files.AddRange(files);
            context.SaveChanges();

            Console.WriteLine("Base data seeded.");
        }
    }
}