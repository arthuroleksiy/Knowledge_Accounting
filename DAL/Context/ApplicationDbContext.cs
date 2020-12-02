using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Linq;
using DAL.Entities.Results;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {/*
        public ApplicationDbContext() : base()
        {

        }
        */

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }



        public DbSet<AllTest> AllTests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Knowledge> Knowledges { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionResult> QuestionResults { get; set; }
        public DbSet<KnowledgeResult> KnowledgeResults { get; set; }
        public DbSet<AnswerResult> AnswerResults { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = KnowledgeSystem4; Integrated Security = True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

           //modelBuilder.Entity<Question>().ToTable("Question");

            //modelBuilder.Entity<Question>().ToTable("ResultQuestion");
            //modelBuilder.Entity<Question>().ToTable("TestQuestion");
            
        


            modelBuilder.Entity<Answer>().HasKey(g => g.AnswerId);
            modelBuilder.Entity<Answer>().HasOne(i => i.Question).WithMany(i => i.Answers).HasForeignKey(i => i.QuestionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Question>().HasKey(g => g.QuestionId);
            //modelBuilder.Entity<Question>().HasMany(i => i.Answers).WithOne(i => i.Question).HasForeignKey(j => j.QuestionId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Question>().HasOne(i => i.CorrectAnswer).WithOne(i => i.CorrectQuestion).HasForeignKey(o => o).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Question>().HasOne(i => i.UserQuestion).WithOne(i => i.UsersAnswer).HasForeignKey<Question>(i => i.  ).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Answer>().HasOne(i => i.CorrectQuestion).WithOne(i => i.CorrectAnswer).HasForeignKey<Answer>(o => o.).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Answer>().HasOne(i => i.UserQuestion).WithOne(i => i.UsersAnswer).HasForeignKey<Answer>(i => i.).OnDelete(DeleteBehavior.Restrict);
           // modelBuilder.Entity<Question>().HasOne(i => i.CorrectAnswer).WithOne(i=> i.CorrectQuestion).HasForeignKey<Answer>(p => p.CorrectQuestionId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Question>().HasOne(i => i.UsersAnswer).WithOne(i => i.UserQuestion);
            //modelBuilder.Entity<Answer>().HasOne<Question>(i => i.Question).WithOne(i => i.CorrectAnswer).HasForeignKey<Question>(i => i.QuestionId);
            //modelBuilder.Entity<Answer>().HasOne<Question>(i => i.CorrectQuestion).WithOne(i => i.).HasForeignKey<Question>(i => i.QuestionId);
            modelBuilder.Entity<Knowledge>().HasKey(g => g.KnowledgeId);
            modelBuilder.Entity<Knowledge>().HasMany(q => q.Questions).WithOne(q => q.Knowledge).HasForeignKey(i => i.KnowledgeId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<QuestionResult>().HasOne(i => i.Question).WithMany(i => i.QuestionResults).HasForeignKey(i => i.QuestionId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<AnswerResult>().HasOne(i => i.QuestionResult).WithOne(i => i.AnswerResult).HasForeignKey<QuestionResult>(i => i.QuestionResultId).OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<QuestionResult>().HasOne(i => i.Question).WithMany(i => i.QuestionResults).HasForeignKey(i => i.QuestionId).OnDelete(DeleteBehavior.Restrict);

            /* 
             modelBuilder.Entity<KnowledgeUser>()
                    .HasKey(t => new { t.KnowledgeId, t.UserId });

             modelBuilder.Entity<KnowledgeUser>()
                 .HasOne(ub => ub.User)
                 .WithMany()
                 .HasForeignKey(ub => ub.UserId).OnDelete(DeleteBehavior.Restrict);

             modelBuilder.Entity<KnowledgeUser>()
                 .HasOne(ub => ub.Knowledge)
                 .WithMany()
                 .HasForeignKey(ub => ub.KnowledgeId).OnDelete(DeleteBehavior.Restrict);
             */
            //modelBuilder.Entity<ResultQuestion>().HasOne(i => i.UsersAnswer).WithOne(i => i.ResultQuestion).HasForeignKey<Answer>(j => j.ResultQuestionId).OnDelete(DeleteBehavior.Restrict); ;
            //modelBuilder.Entity<TestQuestion>().HasOne(i => i.CorrectAnswer).WithOne(i => i.TestQuestion).HasForeignKey<Answer>(j => j.TestQuestionId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<ResultQuestion>().HasOne(i => i.TestResult).WithMany(i => i.ResultQuestions).HasForeignKey(j => j.TestResultId);
            //modelBuilder.Entity<TestQuestion>().HasOne(i => i.AllTest).WithMany(i => i.TestQuestions).HasForeignKey(j => j.AllTestId);

            //modelBuilder.Entity<ResultQuestion>().HasOne(q => q.TestResult).WithMany().HasForeignKey(o => o.KnowledgeId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<TestQuestion>().HasOne(q => q.AllTest).WithMany().HasForeignKey(o => o.KnowledgeId).OnDelete(DeleteBehavior.Restrict);

            /*
                        modelBuilder.Entity<Question>().HasData(new Question { QuestionId = 0, AnswerVariants = new List<string> { ":", "::", ".", "#" }, CorrectAnswer = "." },
                           new Question { QuestionId = 1, AnswerVariants = new List<string> { "1", "2", "Any number", "None of the above" }, CorrectAnswer = "Any number" },
                           new Question { QuestionId = 2, AnswerVariants = new List<string> { "‘==’ operator is used to assign values from one variable to another variable, ‘=’ operator is used to compare value between two variables", "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables", "No difference between both operators", "None of the mentioned" }, CorrectAnswer = "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables" },
                           new Question { QuestionId = 3, AnswerVariants = new List<string> { "Constructor", "Finalize()", "Destructor", "End" }, CorrectAnswer = "Destructor" },
                           new Question { QuestionId = 4, AnswerVariants = new List<string> { ">=", "!=", "<=", "<>=" }, CorrectAnswer = "<>=" });


                        modelBuilder.Entity<Answer>().HasData (  new Answer { AnswerId = 0 = new List<string> { ":", "::", ".", "#" }, CorrectAnswer = "."},
                            new Answer { AnswerId = 1, AnswerVariants = new List<string> { "1", "2", "Any number", "None of the above" }, CorrectAnswer = "Any number" },
                            new Answer { AnswerId = 2, AnswerVariants = new List<string> { "‘==’ operator is used to assign values from one variable to another variable, ‘=’ operator is used to compare value between two variables", "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables", "No difference between both operators", "None of the mentioned" }, CorrectAnswer = "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables" },
                            new Answer { AnswerId = 3, AnswerVariants = new List<string> { "Constructor", "Finalize()", "Destructor", "End" }, CorrectAnswer = "Destructor" },
                            new Answer { AnswerId = 4, AnswerVariants = new List<string> { ">=", "!=", "<=", "<>=" }, CorrectAnswer = "<>=" });*/


            /*
            modelBuilder.Entity<Answer>().HasData(new Answer[]
            {
                
                //C# essentials
                new Answer {AnswerId = 1, AnswerString = ":", QuestionId = 1},
                new Answer {AnswerId = 2, AnswerString =  "::", QuestionId = 1},
                new Answer {AnswerId = 3, AnswerString = ".", QuestionId = 1},
                new Answer {AnswerId = 4, AnswerString = "#", QuestionId = 1},
                new Answer {AnswerId = 5, AnswerString = "1", QuestionId = 2 },
                new Answer {AnswerId = 6, AnswerString = "2", QuestionId = 2},
                new Answer {AnswerId = 7, AnswerString = "Any number", QuestionId = 2},
                new Answer {AnswerId = 8, AnswerString = "None of the above", QuestionId = 2},
                new Answer {AnswerId = 9, AnswerString = "‘==’ operator is used to assign values from one variable to another variable,  ‘=’ operator is used to compare value between two variables", QuestionId = 3 },
                new Answer {AnswerId = 10, AnswerString = "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables", QuestionId = 3},
                new Answer {AnswerId = 11, AnswerString = "No difference between both operators", QuestionId = 3},
                new Answer {AnswerId = 12, AnswerString = "None of the above", QuestionId = 3},
                new Answer {AnswerId = 13, AnswerString = "Destructor", QuestionId = 4 },
                new Answer {AnswerId = 14, AnswerString = "Finalize()", QuestionId = 4},
                new Answer {AnswerId = 15, AnswerString = "Constructor", QuestionId = 4},
                new Answer {AnswerId = 16, AnswerString = "End", QuestionId = 4},
                new Answer {AnswerId = 17, AnswerString = ">=", QuestionId = 5 },
                new Answer {AnswerId = 18, AnswerString = "!=", QuestionId = 5},
                new Answer {AnswerId = 19, AnswerString = "<=", QuestionId = 5},
                new Answer {AnswerId = 20, AnswerString = "<>=", QuestionId = 5},

                //Javascript

                new Answer {AnswerId = 21, AnswerString = "var x = myFunc()", QuestionId = 6},
                new Answer {AnswerId = 22, AnswerString =  "myfunc", QuestionId = 6},
                new Answer {AnswerId = 23, AnswerString = "x = myfunc()", QuestionId = 6},
                new Answer {AnswerId = 24, AnswerString = "myfunc()", QuestionId = 6},
                new Answer {AnswerId = 25, AnswerString = "string \"10\"", QuestionId = 7 },
                new Answer {AnswerId = 26, AnswerString = "array of 10 empty strings", QuestionId = 7},
                new Answer {AnswerId = 27, AnswerString = "string \"..........\"", QuestionId = 7},
                new Answer {AnswerId = 28, AnswerString = "This statement will cause an error", QuestionId = 7},
                new Answer {AnswerId = 29, AnswerString = "defines a new two-dimentional array a whose dimentions are 2 and 4", QuestionId = 8 },
                new Answer {AnswerId = 30, AnswerString = "defines an array a and assigns the values 2 and 4 to a[1] and a[2]", QuestionId = 8},
                new Answer {AnswerId = 31, AnswerString = "defines an array a andd assigns the values 2 and 4 to a[0] and a[1]", QuestionId = 8},
                new Answer {AnswerId = 32, AnswerString = "defines a three-element array whose elements have indexes 2 through 4", QuestionId = 8},
                new Answer {AnswerId = 33, AnswerString = "0", QuestionId = 9 },
                new Answer {AnswerId = 34, AnswerString = "null", QuestionId = 9},
                new Answer {AnswerId = 35, AnswerString = "No output", QuestionId = 9},
                new Answer {AnswerId = 36, AnswerString = "Object", QuestionId = 9},
                new Answer {AnswerId = 37, AnswerString = "var obj = {};", QuestionId =10 },
                new Answer {AnswerId = 38, AnswerString = "var obj = { name: \"Steve\"};", QuestionId = 10},
                new Answer {AnswerId = 39, AnswerString = "var obj = { name = \"Steve\"};", QuestionId = 10},
                new Answer {AnswerId = 40, AnswerString = "var obj = new Object();", QuestionId = 10},

                //SQL
                
                new Answer {AnswerId = 41, AnswerString = "select * from Persons where FirstName Like \'a%\'", QuestionId = 11},
                new Answer {AnswerId = 42, AnswerString = "select * from Persons where FirstName=\'a\'", QuestionId = 11},
                new Answer {AnswerId = 43, AnswerString = "select * from Persons where FirstName Like \'%a\'", QuestionId = 11},
                new Answer {AnswerId = 44, AnswerString = "select * from Persons where FirstName = \'%a%\'", QuestionId = 11},
                new Answer {AnswerId = 45, AnswerString = "Delete from Persons where FirstName = \'Peter\'", QuestionId = 12 },
                new Answer {AnswerId = 46, AnswerString = "Delete row FirstName = \'Peter\' from Persons", QuestionId = 12},
                new Answer {AnswerId = 47, AnswerString = "Delete FirstName = \'Peter\' from Persons", QuestionId = 12},
                new Answer {AnswerId = 48, AnswerString = "None of the above", QuestionId = 12},
                new Answer {AnswerId = 49, AnswerString = "Sort by", QuestionId = 13 },
                new Answer {AnswerId = 50, AnswerString = "Order", QuestionId = 13},
                new Answer {AnswerId = 51, AnswerString = "Order by", QuestionId = 13},
                new Answer {AnswerId = 52, AnswerString = "Sort", QuestionId = 13},
                new Answer {AnswerId = 53, AnswerString = "GET", QuestionId = 14 },
                new Answer {AnswerId = 54, AnswerString = "FROM", QuestionId = 14},
                new Answer {AnswerId = 55, AnswerString = "LIKE", QuestionId = 14},
                new Answer {AnswerId = 56, AnswerString = "End", QuestionId = 14},
                new Answer {AnswerId = 57, AnswerString = "Select distinct", QuestionId = 15 },
                new Answer {AnswerId = 58, AnswerString = "Select unique", QuestionId = 15},
                new Answer {AnswerId = 59, AnswerString = "Select Different", QuestionId = 15},
                new Answer {AnswerId = 60, AnswerString = "<>=", QuestionId = 15},

                 //OOP
                
                new Answer {AnswerId = 61, AnswerString = "class employee : data {}", QuestionId = 16},
                new Answer {AnswerId = 62, AnswerString = "class employee implements data {}", QuestionId = 16},
                new Answer {AnswerId = 63, AnswerString = "class employee imports data {}", QuestionId = 16},
                new Answer {AnswerId = 64, AnswerString = "None of the mentioned", QuestionId = 16},
                new Answer {AnswerId = 65, AnswerString = "Public", QuestionId = 17 },
                new Answer {AnswerId = 66, AnswerString = "Protected", QuestionId = 17},
                new Answer {AnswerId = 67, AnswerString = "Private", QuestionId = 17},
                new Answer {AnswerId = 68, AnswerString = "All of the above", QuestionId = 17},
                new Answer {AnswerId = 69, AnswerString = "upper", QuestionId = 18 },
                new Answer {AnswerId = 70, AnswerString = "base", QuestionId = 18},
                new Answer {AnswerId = 71, AnswerString = "this", QuestionId = 18},
                new Answer {AnswerId = 72, AnswerString = "None of the above", QuestionId = 18},
                new Answer {AnswerId = 73, AnswerString = "overloads", QuestionId = 19 },
                new Answer {AnswerId = 74, AnswerString = "overrides", QuestionId = 19},
                new Answer {AnswerId = 75, AnswerString = "new", QuestionId = 19},
                new Answer {AnswerId = 76, AnswerString = "base", QuestionId = 19},
                new Answer {AnswerId =77, AnswerString = "Static class", QuestionId = 20 },
                new Answer {AnswerId = 78, AnswerString = "Sealed class", QuestionId = 20},
                new Answer {AnswerId =79, AnswerString = "Abstract class", QuestionId = 20},
                new Answer {AnswerId = 80, AnswerString = "Derived class", QuestionId = 20},
            });
            */

            /*
            modelBuilder.Entity<TestQuestion>().HasData(new TestQuestion[]
            {
                new TestQuestion {QuestionId = 1, QuestionString = "Which of the following operator can be used to access the member function of a class", CorrectAnswerId = 2,  KnowledgeId= 0 },
                new TestQuestion {QuestionId = 2, QuestionString = "Which of the following gives the correct count of the constructors that a class can define?" ,CorrectAnswerId =6,  KnowledgeId= 0 },
                new TestQuestion {QuestionId = 3, QuestionString = "Which of the following statements correctly tell the differences between ‘=’ and ‘==’ in C#", CorrectAnswerId = 9,  KnowledgeId= 0},
                new TestQuestion {QuestionId = 4, QuestionString = "What is the correct name of a method which has the same name as that of class and used to destroy objects?", CorrectAnswerId =12,  KnowledgeId= 0 },
                new TestQuestion {QuestionId = 5, QuestionString = "Which of the following operator is not an equality operators" ,CorrectAnswerId =19,  KnowledgeId= 0 },
                new TestQuestion {QuestionId = 6, QuestionString = "Which of the folowing is not a valid function call?", CorrectAnswerId =21,  KnowledgeId= 1 },
                new TestQuestion {QuestionId = 7, QuestionString = "After executing the Javascript statement a=(new Araray(10)).toString(), what is the value of a?" ,CorrectAnswerId =26,  KnowledgeId= 1 },
                new TestQuestion {QuestionId = 8, QuestionString = "The JavaScipt statement a = new Array(2,4)", CorrectAnswerId = 30,  KnowledgeId= 1 },
                new TestQuestion {QuestionId = 9, QuestionString = "var x = 0;\ndo\n{ console.log(x) } \nwhile (x > 0)?", CorrectAnswerId =32,  KnowledgeId= 1 },
                new TestQuestion {QuestionId = 10, QuestionString = "Which of the following is NOT a JavaScript object?" ,CorrectAnswerId =38,  KnowledgeId= 1 },
                new TestQuestion {QuestionId = 11, QuestionString = "With SQL, how do you select a column named FirstName from a table named Person, whhree the alue of the column FirstNamae starts with \"a\"?", CorrectAnswerId =42,  KnowledgeId= 2 },
                new TestQuestion {QuestionId = 12, QuestionString = "With SQL, how can you delete the records where the FirstName is Peter in the Persons Table?" ,CorrectAnswerId =44,  KnowledgeId= 2  },
                new TestQuestion {QuestionId = 13, QuestionString = "Which SQL keyword is used to sort the result-set", CorrectAnswerId =50,  KnowledgeId= 2  },
                new TestQuestion {QuestionId = 14, QuestionString = "Which operator is used to search for a specified pattern in a column?", CorrectAnswerId =54 ,  KnowledgeId= 2 },
                new TestQuestion {QuestionId = 15, QuestionString = "Which SQL statement is used to return only different values?" ,CorrectAnswerId =56,  KnowledgeId= 2  },
                new TestQuestion {QuestionId = 16, QuestionString = "Which of the following options define the correct way of implementing an interface data by the class employee?", CorrectAnswerId =60 ,  KnowledgeId= 3 },
                new TestQuestion {QuestionId = 17, QuestionString = "Which of the following Access specifiers can be used for an interface?" ,CorrectAnswerId =64,  KnowledgeId= 3  },
                new TestQuestion {QuestionId = 18, QuestionString = "Which of the following keywords can be used to access a member of the base class from derived class?", CorrectAnswerId =69,  KnowledgeId= 3  },
                new TestQuestion {QuestionId = 19, QuestionString = "Which of the following keyword, enables to modify the data and behavior of a base class by replacing its member with a new derived member?", CorrectAnswerId =74,  KnowledgeId= 3  },
                new TestQuestion {QuestionId = 20, QuestionString = "Which of the following options represents the type of class which does not have its own objects but acts as a base class for its subclass?" ,CorrectAnswerId =78,  KnowledgeId= 3  },
                
            });*/


            /*
            List<Answer> list = new List<Answer>() {  
                //C# essentials
                new Answer {AnswerId = 1, AnswerString = ":"},
                new Answer {AnswerId = 2, AnswerString =  "::"},
                new Answer {AnswerId = 3, AnswerString = "."},
                new Answer {AnswerId = 4, AnswerString = "#"},
                new Answer {AnswerId = 5, AnswerString = "1" },
                new Answer {AnswerId = 6, AnswerString = "2"},
                new Answer {AnswerId = 7, AnswerString = "Any number"},
                new Answer {AnswerId = 8, AnswerString = "None of the above"},
                new Answer {AnswerId = 9, AnswerString = "‘==’ operator is used to assign values from one variable to another variable,  ‘=’ operator is used to compare value between two variables"},
                new Answer {AnswerId = 10, AnswerString = "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables"},
                new Answer {AnswerId = 11, AnswerString = "No difference between both operators"},
                new Answer {AnswerId = 12, AnswerString = "None of the above"},
                new Answer {AnswerId = 13, AnswerString = "Destructor" },
                new Answer {AnswerId = 14, AnswerString = "Finalize()"},
                new Answer {AnswerId = 15, AnswerString = "Constructor"},
                new Answer {AnswerId = 16, AnswerString = "End"},
                new Answer {AnswerId = 17, AnswerString = ">=" },
                new Answer {AnswerId = 18, AnswerString = "!="},
                new Answer {AnswerId = 19, AnswerString = "<="},
                new Answer {AnswerId = 20, AnswerString = "<>="},

                //Javascript

                new Answer {AnswerId = 21, AnswerString = "var x = myFunc()"},
                new Answer {AnswerId = 22, AnswerString =  "myfunc"},
                new Answer {AnswerId = 23, AnswerString = "x = myfunc()"},
                new Answer {AnswerId = 24, AnswerString = "myfunc()"},
                new Answer {AnswerId = 25, AnswerString = "string \"10\"" },
                new Answer {AnswerId = 26, AnswerString = "array of 10 empty strings"},
                new Answer {AnswerId = 27, AnswerString = "string \"..........\""},
                new Answer {AnswerId = 28, AnswerString = "This statement will cause an error"},
                new Answer {AnswerId = 29, AnswerString = "defines a new two-dimentional array a whose dimentions are 2 and 4" },
                new Answer {AnswerId = 30, AnswerString = "defines an array a and assigns the values 2 and 4 to a[1] and a[2]"},
                new Answer {AnswerId = 31, AnswerString = "defines an array a andd assigns the values 2 and 4 to a[0] and a[1]" },
                new Answer {AnswerId = 32, AnswerString = "defines a three-element array whose elements have indexes 2 through 4"},
                new Answer {AnswerId = 33, AnswerString = "0" },
                new Answer {AnswerId = 34, AnswerString = "null"},
                new Answer {AnswerId = 35, AnswerString = "No output"},
                new Answer {AnswerId = 36, AnswerString = "Object"},
                new Answer {AnswerId = 37, AnswerString = "var obj = {};" },
                new Answer {AnswerId = 38, AnswerString = "var obj = { name: \"Steve\"};"},
                new Answer {AnswerId = 39, AnswerString = "var obj = { name = \"Steve\"};"},
                new Answer {AnswerId = 40, AnswerString = "var obj = new Object();"},

                //SQL
                
                new Answer {AnswerId = 41, AnswerString = "select * from Persons where FirstName Like \'a%\'"},
                new Answer {AnswerId = 42, AnswerString = "select * from Persons where FirstName=\'a\'"},
                new Answer {AnswerId = 43, AnswerString = "select * from Persons where FirstName Like \'%a\'"},
                new Answer {AnswerId = 44, AnswerString = "select * from Persons where FirstName = \'%a%\'"},
                new Answer {AnswerId = 45, AnswerString = "Delete from Persons where FirstName = \'Peter\'" },
                new Answer {AnswerId = 46, AnswerString = "Delete row FirstName = \'Peter\' from Persons"},
                new Answer {AnswerId = 47, AnswerString = "Delete FirstName = \'Peter\' from Persons"},
                new Answer {AnswerId = 48, AnswerString = "None of the above"},
                new Answer {AnswerId = 49, AnswerString = "Sort by" },
                new Answer {AnswerId = 50, AnswerString = "Order"},
                new Answer {AnswerId = 51, AnswerString = "Order by"},
                new Answer {AnswerId = 52, AnswerString = "Sort"},
                new Answer {AnswerId = 53, AnswerString = "GET"},
                new Answer {AnswerId = 54, AnswerString = "FROM"},
                new Answer {AnswerId = 55, AnswerString = "LIKE"},
                new Answer {AnswerId = 56, AnswerString = "End"},
                new Answer {AnswerId = 57, AnswerString = "Select distinct" },
                new Answer {AnswerId = 58, AnswerString = "Select unique"},
                new Answer {AnswerId = 59, AnswerString = "Select Different"},
                new Answer {AnswerId = 60, AnswerString = "<>="},

                 //OOP
                
                new Answer {AnswerId = 61, AnswerString = "class employee : data {}"},
                new Answer {AnswerId = 62, AnswerString = "class employee implements data {}"},
                new Answer {AnswerId = 63, AnswerString = "class employee imports data {}"},
                new Answer {AnswerId = 64, AnswerString = "None of the mentioned"},
                new Answer {AnswerId = 65, AnswerString = "Public" },
                new Answer {AnswerId = 66, AnswerString = "Protected"},
                new Answer {AnswerId = 67, AnswerString = "Private"},
                new Answer {AnswerId = 68, AnswerString = "All of the above"},
                new Answer {AnswerId = 69, AnswerString = "upper"},
                new Answer {AnswerId = 70, AnswerString = "base"},
                new Answer {AnswerId = 71, AnswerString = "this"},
                new Answer {AnswerId = 72, AnswerString = "None of the above"},
                new Answer {AnswerId = 73, AnswerString = "overloads"},
                new Answer {AnswerId = 74, AnswerString = "overrides"},
                new Answer {AnswerId = 75, AnswerString = "new"},
                new Answer {AnswerId = 76, AnswerString = "base"},
                new Answer {AnswerId =77, AnswerString = "Static class"},
                new Answer {AnswerId = 78, AnswerString = "Sealed class"},
                new Answer {AnswerId =79, AnswerString = "Abstract class"},
                new Answer {AnswerId = 80, AnswerString = "Derived class"}
            };
            */

            /*modelBuilder.Entity<AllTest>().HasData(new AllTest[]
            {
                new AllTest { KnowledgeId = 1, KnowledgeName= "C# essentials"},
                new AllTest { KnowledgeId = 2, KnowledgeName= "Javascript" },
                new AllTest { KnowledgeId = 3, KnowledgeName= "SQL"},
                new AllTest { KnowledgeId = 4, KnowledgeName= "OOP" }
            });*/

            //modelBuilder.Entity<Answer>().HasData(new Answer[]{});
            /*List<Answer> answers = new List<Answer>()
            {  
                //C# essentials
                new Answer {AnswerId = 1, AnswerString = ":", QuestionId = 1},
                new Answer {AnswerId = 2, AnswerString =  "::", QuestionId = 1},
                new Answer {AnswerId = 3, AnswerString = ".", QuestionId = 1},
                new Answer {AnswerId = 4, AnswerString = "#", QuestionId = 1},
                new Answer {AnswerId = 5, AnswerString = "1", QuestionId = 2 },
                new Answer {AnswerId = 6, AnswerString = "2", QuestionId = 2},
                new Answer {AnswerId = 7, AnswerString = "Any number", QuestionId = 2},
                new Answer {AnswerId = 8, AnswerString = "None of the above", QuestionId = 2},
                new Answer {AnswerId = 9, AnswerString = "‘==’ operator is used to assign values from one variable to another variable,  ‘=’ operator is used to compare value between two variables", QuestionId = 3 },
                new Answer {AnswerId = 10, AnswerString = "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables", QuestionId = 3},
                new Answer {AnswerId = 11, AnswerString = "No difference between both operators", QuestionId = 3},
                new Answer {AnswerId = 12, AnswerString = "None of the above", QuestionId = 3},
                new Answer {AnswerId = 13, AnswerString = "Destructor", QuestionId = 4 },
                new Answer {AnswerId = 14, AnswerString = "Finalize()", QuestionId = 4},
                new Answer {AnswerId = 15, AnswerString = "Constructor", QuestionId = 4},
                new Answer {AnswerId = 16, AnswerString = "End", QuestionId = 4},
                new Answer {AnswerId = 17, AnswerString = ">=", QuestionId = 5 },
                new Answer {AnswerId = 18, AnswerString = "!=", QuestionId = 5},
                new Answer {AnswerId = 19, AnswerString = "<=", QuestionId = 5},
                new Answer {AnswerId = 20, AnswerString = "<>=", QuestionId = 5},
            
                //Javascript

                new Answer {AnswerId = 21, AnswerString = "var x = myFunc()", QuestionId = 6},
                new Answer {AnswerId = 22, AnswerString =  "myfunc", QuestionId = 6},
                new Answer {AnswerId = 23, AnswerString = "x = myfunc()", QuestionId = 6},
                new Answer {AnswerId = 24, AnswerString = "myfunc()", QuestionId = 6},
                new Answer {AnswerId = 25, AnswerString = "string \"10\"", QuestionId = 7 },
                new Answer {AnswerId = 26, AnswerString = "array of 10 empty strings", QuestionId = 7},
                new Answer {AnswerId = 27, AnswerString = "string \"..........\"", QuestionId = 7},
                new Answer {AnswerId = 28, AnswerString = "This statement will cause an error", QuestionId = 7},
                new Answer {AnswerId = 29, AnswerString = "defines a new two-dimentional array a whose dimentions are 2 and 4", QuestionId = 8 },
                new Answer {AnswerId = 30, AnswerString = "defines an array a and assigns the values 2 and 4 to a[1] and a[2]", QuestionId = 8},
                new Answer {AnswerId = 31, AnswerString = "defines an array a andd assigns the values 2 and 4 to a[0] and a[1]", QuestionId = 8},
                new Answer {AnswerId = 32, AnswerString = "defines a three-element array whose elements have indexes 2 through 4", QuestionId = 8},
                new Answer {AnswerId = 33, AnswerString = "0", QuestionId = 9 },
                new Answer {AnswerId = 34, AnswerString = "null", QuestionId = 9},
                new Answer {AnswerId = 35, AnswerString = "No output", QuestionId = 9},
                new Answer {AnswerId = 36, AnswerString = "Object", QuestionId = 9},
                new Answer {AnswerId = 37, AnswerString = "var obj = {};", QuestionId =10 },
                new Answer {AnswerId = 38, AnswerString = "var obj = { name: \"Steve\"};", QuestionId = 10},
                new Answer {AnswerId = 39, AnswerString = "var obj = { name = \"Steve\"};", QuestionId = 10},
                new Answer {AnswerId = 40, AnswerString = "var obj = new Object();", QuestionId = 10},

                //SQL
                
                new Answer {AnswerId = 41, AnswerString = "select * from Persons where FirstName Like \'a%\'", QuestionId = 11},
                new Answer {AnswerId = 42, AnswerString = "select * from Persons where FirstName=\'a\'", QuestionId = 11},
                new Answer {AnswerId = 43, AnswerString = "select * from Persons where FirstName Like \'%a\'", QuestionId = 11},
                new Answer {AnswerId = 44, AnswerString = "select * from Persons where FirstName = \'%a%\'", QuestionId = 11},
                new Answer {AnswerId = 45, AnswerString = "Delete from Persons where FirstName = \'Peter\'", QuestionId = 12 },
                new Answer {AnswerId = 46, AnswerString = "Delete row FirstName = \'Peter\' from Persons", QuestionId = 12},
                new Answer {AnswerId = 47, AnswerString = "Delete FirstName = \'Peter\' from Persons", QuestionId = 12},
                new Answer {AnswerId = 48, AnswerString = "None of the above", QuestionId = 12},
                new Answer {AnswerId = 49, AnswerString = "Sort by", QuestionId = 13 },
                new Answer {AnswerId = 50, AnswerString = "Order", QuestionId = 13},
                new Answer {AnswerId = 51, AnswerString = "Order by", QuestionId = 13},
                new Answer {AnswerId = 52, AnswerString = "Sort", QuestionId = 13},
                new Answer {AnswerId = 53, AnswerString = "GET", QuestionId = 14 },
                new Answer {AnswerId = 54, AnswerString = "FROM", QuestionId = 14},
                new Answer {AnswerId = 55, AnswerString = "LIKE", QuestionId = 14},
                new Answer {AnswerId = 56, AnswerString = "End", QuestionId = 14},
                new Answer {AnswerId = 57, AnswerString = "Select distinct", QuestionId = 15 },
                new Answer {AnswerId = 58, AnswerString = "Select unique", QuestionId = 15},
                new Answer {AnswerId = 59, AnswerString = "Select Different", QuestionId = 15},
                new Answer {AnswerId = 60, AnswerString = "<>=", QuestionId = 15},

                 //OOP
                
                new Answer {AnswerId = 61, AnswerString = "class employee : data {}", QuestionId = 16},
                new Answer {AnswerId = 62, AnswerString = "class employee implements data {}", QuestionId = 16},
                new Answer {AnswerId = 63, AnswerString = "class employee imports data {}", QuestionId = 16},
                new Answer {AnswerId = 64, AnswerString = "None of the mentioned", QuestionId = 16},
                new Answer {AnswerId = 65, AnswerString = "Public", QuestionId = 17 },
                new Answer {AnswerId = 66, AnswerString = "Protected", QuestionId = 17},
                new Answer {AnswerId = 67, AnswerString = "Private", QuestionId = 17},
                new Answer {AnswerId = 68, AnswerString = "All of the above", QuestionId = 17},
                new Answer {AnswerId = 69, AnswerString = "upper", QuestionId = 18 },
                new Answer {AnswerId = 70, AnswerString = "base", QuestionId = 18},
                new Answer {AnswerId = 71, AnswerString = "this", QuestionId = 18},
                new Answer {AnswerId = 72, AnswerString = "None of the above", QuestionId = 18},
                new Answer {AnswerId = 73, AnswerString = "overloads", QuestionId = 19 },
                new Answer {AnswerId = 74, AnswerString = "overrides", QuestionId = 19},
                new Answer {AnswerId = 75, AnswerString = "new", QuestionId = 19},
                new Answer {AnswerId = 76, AnswerString = "base", QuestionId = 19},
                new Answer {AnswerId =77, AnswerString = "Static class", QuestionId = 20 },
                new Answer {AnswerId = 78, AnswerString = "Sealed class", QuestionId = 20},
                new Answer {AnswerId =79, AnswerString = "Abstract class", QuestionId = 20},
                new Answer {AnswerId = 80, AnswerString = "Derived class", QuestionId = 20, },
            };
            */
            /*
            List<Answer> answers = new List<Answer>() {  
                //C# essentials
                new Answer { AnswerString = ":"},
                new Answer { AnswerString =  "::"},
                new Answer { AnswerString = "."},
                new Answer { AnswerString = "#"},
                new Answer { AnswerString = "1" },
                new Answer { AnswerString = "2"},
                new Answer { AnswerString = "Any number"},
                new Answer {AnswerString = "None of the above"},
                new Answer { AnswerString = "‘==’ operator is used to assign values from one variable to another variable,  ‘=’ operator is used to compare value between two variables"},
                new Answer { AnswerString = "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables"},
                new Answer { AnswerString = "No difference between both operators"},
                new Answer { AnswerString = "None of the above"},
                new Answer { AnswerString = "Destructor" },
                new Answer { AnswerString = "Finalize()"},
                new Answer { AnswerString = "Constructor"},
                new Answer { AnswerString = "End"},
                new Answer {AnswerString = ">=" },
                new Answer { AnswerString = "!="},
                new Answer { AnswerString = "<="},
                new Answer { AnswerString = "<>="},

                //Javascript

                new Answer { AnswerString = "var x = myFunc()"},
                new Answer { AnswerString =  "myfunc"},
                new Answer { AnswerString = "x = myfunc()"},
                new Answer { AnswerString = "myfunc()"},
                new Answer { AnswerString = "string \"10\"" },
                new Answer { AnswerString = "array of 10 empty strings"},
                new Answer { AnswerString = "string \"..........\""},
                new Answer {AnswerString = "This statement will cause an error"},
                new Answer { AnswerString = "defines a new two-dimentional array a whose dimentions are 2 and 4" },
                new Answer { AnswerString = "defines an array a and assigns the values 2 and 4 to a[1] and a[2]"},
                new Answer {AnswerString = "defines an array a andd assigns the values 2 and 4 to a[0] and a[1]" },
                new Answer { AnswerString = "defines a three-element array whose elements have indexes 2 through 4"},
                new Answer { AnswerString = "0" },
                new Answer { AnswerString = "null"},
                new Answer { AnswerString = "No output"},
                new Answer { AnswerString = "Object"},
                new Answer { AnswerString = "var obj = {};" },
                new Answer { AnswerString = "var obj = { name: \"Steve\"};"},
                new Answer { AnswerString = "var obj = { name = \"Steve\"};"},
                new Answer { AnswerString = "var obj = new Object();"},

                //SQL
                
                new Answer { AnswerString = "select * from Persons where FirstName Like \'a%\'"},
                new Answer {AnswerString = "select * from Persons where FirstName=\'a\'"},
                new Answer { AnswerString = "select * from Persons where FirstName Like \'%a\'"},
                new Answer { AnswerString = "select * from Persons where FirstName = \'%a%\'"},
                new Answer { AnswerString = "Delete from Persons where FirstName = \'Peter\'" },
                new Answer { AnswerString = "Delete row FirstName = \'Peter\' from Persons"},
                new Answer { AnswerString = "Delete FirstName = \'Peter\' from Persons"},
                new Answer { AnswerString = "None of the above"},
                new Answer {AnswerString = "Sort by" },
                new Answer { AnswerString = "Order"},
                new Answer { AnswerString = "Order by"},
                new Answer {AnswerString = "Sort"},
                new Answer { AnswerString = "GET"},
                new Answer { AnswerString = "FROM"},
                new Answer { AnswerString = "LIKE"},
                new Answer {AnswerString = "End"},
                new Answer {AnswerString = "Select distinct" },
                new Answer {AnswerString = "Select unique"},
                new Answer {AnswerString = "Select Different"},
                new Answer { AnswerString = "<>="},

                 //OOP
                
                new Answer { AnswerString = "class employee : data {}"},
                new Answer { AnswerString = "class employee implements data {}"},
                new Answer { AnswerString = "class employee imports data {}"},
                new Answer { AnswerString = "None of the mentioned"},
                new Answer { AnswerString = "Public" },
                new Answer { AnswerString = "Protected"},
                new Answer { AnswerString = "Private"},
                new Answer {AnswerString = "All of the above"},
                new Answer { AnswerString = "upper"},
                new Answer { AnswerString = "base"},
                new Answer { AnswerString = "this"},
                new Answer { AnswerString = "None of the above"},
                new Answer { AnswerString = "overloads"},
                new Answer { AnswerString = "overrides"},
                new Answer { AnswerString = "new"},
                new Answer { AnswerString = "base"},
                new Answer { AnswerString = "Static class"},
                new Answer { AnswerString = "Sealed class"},
                new Answer { AnswerString = "Abstract class"},
                new Answer { AnswerString = "Derived class"}
            };*/



            /*
            List<Question> testQuestions = new List<Question>() {
                new Question { QuestionString = "Which of the following operator can be used to access the member function of a class", CorrectAnswer = answers.Take(1).First(), Answers =answers.Take(4).ToList() , KnowledgeId = 1 },
                new Question {QuestionString = "Which of the following gives the correct count of the constructors that a class can define?" ,CorrectAnswer = answers.Skip(4).Take(1).First(), Answers =answers.Skip(4).Take(4).ToList() , KnowledgeId = 1 },
                new Question { QuestionString = "Which of the following statements correctly tell the differences between ‘=’ and ‘==’ in C#", CorrectAnswer = answers.Skip(7).Take(1).First(), Answers =answers.Skip(8).Take(4).ToList(), KnowledgeId = 1 },
                new Question { QuestionString = "What is the correct name of a method which has the same name as that of class and used to destroy objects?", CorrectAnswer = answers.Skip(10).Take(1).First(), Answers =answers.Skip(12).Take(4).ToList(), KnowledgeId = 1 },
                new Question { QuestionString = "Which of the following operator is not an equality operators" ,CorrectAnswer = answers.Skip(17).Take(1).First(), Answers =answers.Skip(16).Take(4).ToList(), KnowledgeId = 1 },
                new Question { QuestionString = "Which of the folowing is not a valid function call?", CorrectAnswer = answers.Skip(19).Take(1).First() , Answers =answers.Skip(20).Take(4).ToList(), KnowledgeId = 2 },
                new Question { QuestionString = "After executing the Javascript statement a=(new Araray(10)).toString(), what is the value of a?" ,CorrectAnswer = answers.Skip(24).Take(1).First(), Answers =answers.Skip(24).Take(4).ToList() , KnowledgeId = 2},
                new Question { QuestionString = "The JavaScipt statement a = new Array(2,4)", CorrectAnswer = answers.Skip(28).Take(1).First(), Answers =answers.Skip(28).Take(4).ToList(), KnowledgeId = 2},
                new Question { QuestionString = "var x = 0;\ndo\n{ console.log(x) } \nwhile (x > 0)?", CorrectAnswer = answers.Skip(30).Take(1).First(), Answers =answers.Skip(32).Take(4).ToList(), KnowledgeId = 2 },
                new Question { QuestionString = "Which of the following is NOT a JavaScript object?" ,CorrectAnswer = answers.Skip(36).Take(1).First(), Answers =answers.Skip(36).Take(4).ToList(), KnowledgeId = 2},
                new Question { QuestionString = "With SQL, how do you select a column named FirstName from a table named Person, whhree the alue of the column FirstNamae starts with \"a\"?", CorrectAnswer = answers.Skip(42).Take(1).First(), Answers =answers.Skip(40).Take(4).ToList(), KnowledgeId = 3 },
                new Question { QuestionString = "With SQL, how can you delete the records where the FirstName is Peter in the Persons Table?" ,CorrectAnswer = answers.Skip(43).Take(1).First(), Answers =answers.Skip(45).Take(4).ToList(), KnowledgeId = 3 },
                new Question { QuestionString = "Which SQL keyword is used to sort the result-set", CorrectAnswer = answers.Skip(50).Take(1).First(), Answers =answers.Skip(48).Take(4).ToList(), KnowledgeId = 3},
                new Question { QuestionString = "Which operator is used to search for a specified pattern in a column?", CorrectAnswer = answers.Skip(54).Take(1).First(), Answers =answers.Skip(52).Take(4).ToList(), KnowledgeId = 3 },
                new Question { QuestionString = "Which SQL statement is used to return only different values?" ,CorrectAnswer = answers.Skip(56).Take(1).First(), Answers =answers.Skip(56).Take(4).ToList(), KnowledgeId = 3  },
                new Question { QuestionString = "Which of the following options define the correct way of implementing an interface data by the class employee?", CorrectAnswer = answers.Skip(60).Take(1).First(), Answers =answers.Skip(60).Take(4).ToList(), KnowledgeId = 4 },
                new Question { QuestionString = "Which of the following Access specifiers can be used for an interface?" ,CorrectAnswer = answers.Skip(64).Take(1).First(), Answers =answers.Skip(64).Take(4).ToList(), KnowledgeId = 4  },
                new Question { QuestionString = "Which of the following keywords can be used to access a member of the base class from derived class?", CorrectAnswer = answers.Skip(69).Take(1).First(), Answers =answers.Skip(68).Take(4).ToList(), KnowledgeId = 4  },
                new Question {QuestionString = "Which of the following keyword, enables to modify the data and behavior of a base class by replacing its member with a new derived member?", CorrectAnswer = answers.Skip(74).Take(1).First() , Answers =answers.Skip(72).Take(4).ToList(), KnowledgeId = 4  },
                new Question { QuestionString = "Which of the following options represents the type of class which does not have its own objects but acts as a base class for its subclass?" ,CorrectAnswer = answers.Skip(78).Take(1).First() , Answers =answers.Skip(76).Take(4).ToList() , KnowledgeId = 4  }
            };*/

            List<AllTest> allTest = new List<AllTest>() {
                new AllTest{ AllTestId = 1 }
            };

            modelBuilder.Entity<AllTest>().HasData(allTest);
            List<Knowledge> knowledge = new List<Knowledge>() {
                 new Knowledge { KnowledgeId = 1 , KnowledgeName= "C# essentials", /*Questions = testQuestions.Take(5).ToList(),*/ AllTestId = 1},
                 new Knowledge { KnowledgeId = 2 , KnowledgeName= "Javascript", /*Questions = testQuestions.Skip(5).Take(5).ToList(),*/ AllTestId = 1 },
                 new Knowledge { KnowledgeId = 3 , KnowledgeName= "SQL", /*Questions = testQuestions.Skip(10).Take(5).ToList(),*/ AllTestId = 1 },
                 new Knowledge { KnowledgeId = 4 , KnowledgeName= "OOP" , /*Questions = testQuestions.Skip(15).Take(5).ToList(),*/ AllTestId = 1}
             };

            modelBuilder.Entity<Knowledge>().HasData(knowledge);
            List<Question> testQuestions = new List<Question>() {
                new Question {QuestionId = 1, QuestionString = "Which of the following operator can be used to access the member function of a class",  KnowledgeId = 1},
                new Question {QuestionId = 2, QuestionString = "Which of the following gives the correct count of the constructors that a class can define?" , KnowledgeId = 1 },
                new Question {QuestionId = 3, QuestionString = "Which of the following statements correctly tell the differences between ‘=’ and ‘==’ in C#", KnowledgeId = 1},
                new Question {QuestionId = 4, QuestionString = "What is the correct name of a method which has the same name as that of class and used to destroy objects?", KnowledgeId = 1 },
                new Question {QuestionId = 5, QuestionString = "Which of the following operator is not an equality operators" ,KnowledgeId = 1 },
               new Question {QuestionId = 6, QuestionString = "Which of the folowing is not a valid function call?",   KnowledgeId= 2 },
                new Question {QuestionId = 7, QuestionString = "After executing the Javascript statement a=(new Araray(10)).toString(), what is the value of a?",KnowledgeId = 2 },
                new Question {QuestionId = 8, QuestionString = "The JavaScipt statement a = new Array(2,4)", KnowledgeId = 2},
                new Question {QuestionId = 9, QuestionString = "var x = 0;\ndo\n{ console.log(x) } \nwhile (x > 0)?", KnowledgeId = 2},
                new Question {QuestionId = 10, QuestionString = "Which of the following is NOT a JavaScript object?" ,KnowledgeId = 2},
                new Question {QuestionId = 11, QuestionString = "With SQL, how do you select a column named FirstName from a table named Person, whree the value of the column FirstNamae starts with \"a\"?",KnowledgeId = 3 },
                new Question {QuestionId = 12, QuestionString = "With SQL, how can you delete the records where the FirstName is Peter in the Persons Table?" ,KnowledgeId = 3 },
                new Question {QuestionId = 13, QuestionString = "Which SQL keyword is used to sort the result-set", KnowledgeId = 3 },
                new Question {QuestionId = 14, QuestionString = "Which operator is used to search for a specified pattern in a column?", KnowledgeId = 3  },
                new Question {QuestionId = 15, QuestionString = "Which SQL statement is used to return only different values?" ,KnowledgeId = 3 },
                new Question {QuestionId = 16, QuestionString = "Which of the following options define the correct way of implementing an interface data by the class employee?",KnowledgeId = 4, },
                new Question {QuestionId = 17, QuestionString = "Which of the following Access specifiers can be used for an interface?" , KnowledgeId = 4},
                new Question {QuestionId = 18, QuestionString = "Which of the following keywords can be used to access a member of the base class from derived class?",KnowledgeId = 4  },
                new Question {QuestionId = 19, QuestionString = "Which of the following keyword, enables to modify the data and behavior of a base class by replacing its member with a new derived member?",KnowledgeId = 4 },
                new Question {QuestionId = 20, QuestionString = "Which of the following options represents the type of class which does not have its own objects but acts as a base class for its subclass?" ,KnowledgeId = 4   }
            };

            modelBuilder.Entity<Question>().HasData(testQuestions);
            List<Answer> answers = new List<Answer>() {  
                //C# essentials
                new Answer {AnswerId = 1, AnswerString = ":", QuestionId = 1, CorrectAnswer = false },
                new Answer {AnswerId = 2, AnswerString =  "::", QuestionId = 1, CorrectAnswer = false},
                new Answer {AnswerId = 3, AnswerString = ".", QuestionId = 1, CorrectAnswer = true},
                new Answer {AnswerId = 4, AnswerString = "#", QuestionId = 1, CorrectAnswer = false},
                new Answer {AnswerId = 5, AnswerString = "1" , QuestionId = 2, CorrectAnswer = false},
                new Answer {AnswerId = 6, AnswerString = "2", QuestionId = 2, CorrectAnswer = false},
                new Answer {AnswerId = 7, AnswerString = "Any number", QuestionId = 2 , CorrectAnswer = true},
                new Answer {AnswerId = 8, AnswerString = "None of the above", QuestionId = 2, CorrectAnswer = false},
                new Answer {AnswerId = 9, AnswerString = "‘==’ operator is used to assign values from one variable to another variable,  ‘=’ operator is used to compare value between two variables", QuestionId = 3, CorrectAnswer = false},
                new Answer {AnswerId = 10, AnswerString = "‘=’ operator is used to assign values from one variable to another variable, ‘==’ operator is used to compare value between two variables", QuestionId = 3, CorrectAnswer = true},
                new Answer {AnswerId = 11, AnswerString = "No difference between both operators", QuestionId = 3, CorrectAnswer = false},
                new Answer {AnswerId = 12, AnswerString = "None of the above", QuestionId = 3, CorrectAnswer = false},
                new Answer {AnswerId = 13, AnswerString = "Destructor" , QuestionId = 4, CorrectAnswer = true},
                new Answer {AnswerId = 14, AnswerString = "Finalize()", QuestionId = 4, CorrectAnswer = false},
                new Answer {AnswerId = 15, AnswerString = "Constructor", QuestionId = 4, CorrectAnswer = false},
                new Answer {AnswerId = 16, AnswerString = "End", QuestionId = 4, CorrectAnswer = false},
                new Answer {AnswerId = 17, AnswerString = ">=", QuestionId = 5, CorrectAnswer = false },
                new Answer {AnswerId = 18, AnswerString = "!=", QuestionId = 5, CorrectAnswer = false},
                new Answer {AnswerId = 19, AnswerString = "<=", QuestionId = 5, CorrectAnswer = false},
                new Answer {AnswerId = 20, AnswerString = "<>=", QuestionId = 5, CorrectAnswer = true},

                //Javascript

                new Answer {AnswerId = 21, AnswerString = "var x = myFunc()" , QuestionId = 6, CorrectAnswer = false},
                new Answer {AnswerId = 22, AnswerString =  "myfunc" , QuestionId = 6, CorrectAnswer = true},
                new Answer {AnswerId = 23, AnswerString = "x = myfunc()", QuestionId = 6, CorrectAnswer = false},
                new Answer {AnswerId = 24, AnswerString = "myfunc()", QuestionId = 6, CorrectAnswer = false},
                new Answer {AnswerId = 25, AnswerString = "string \"10\"", QuestionId = 7 , CorrectAnswer = false},
                new Answer {AnswerId = 26, AnswerString = "array of 10 empty strings", QuestionId = 7, CorrectAnswer = false},
                new Answer {AnswerId = 27, AnswerString = "string \"..........\"", QuestionId = 7, CorrectAnswer = true},
                new Answer {AnswerId = 28, AnswerString = "This statement will cause an error", QuestionId = 7, CorrectAnswer = false},
                new Answer {AnswerId = 29, AnswerString = "defines a new two-dimentional array a whose dimentions are 2 and 4" , QuestionId = 8, CorrectAnswer = false},
                new Answer {AnswerId = 30, AnswerString = "defines an array a and assigns the values 2 and 4 to a[1] and a[2]", QuestionId = 8, CorrectAnswer = false},
                new Answer {AnswerId = 31, AnswerString = "defines an array a andd assigns the values 2 and 4 to a[0] and a[1]", QuestionId = 8 , CorrectAnswer = true},
                new Answer {AnswerId = 32, AnswerString = "defines a three-element array whose elements have indexes 2 through 4", QuestionId = 8, CorrectAnswer = false },
                new Answer {AnswerId = 33, AnswerString = "0" , QuestionId = 9, CorrectAnswer = true },
                new Answer {AnswerId = 34, AnswerString = "null", QuestionId = 9, CorrectAnswer = false},
                new Answer {AnswerId = 35, AnswerString = "No output",QuestionId = 9, CorrectAnswer = false},
                new Answer {AnswerId = 36, AnswerString = "Object", QuestionId = 9, CorrectAnswer = false},
                new Answer {AnswerId = 37, AnswerString = "var obj = {};", QuestionId = 10, CorrectAnswer = false},
                new Answer {AnswerId = 38, AnswerString = "var obj = { name: \"Steve\"};", QuestionId = 10, CorrectAnswer = false},
                new Answer {AnswerId = 39, AnswerString = "var obj = { name = \"Steve\"};", QuestionId = 10, CorrectAnswer = true},
                new Answer {AnswerId = 40, AnswerString = "var obj = new Object();", QuestionId = 10, CorrectAnswer = false},

                //SQL
                
                new Answer {AnswerId = 41, AnswerString = "select * from Persons where FirstName Like \'a%\'", QuestionId = 11, CorrectAnswer = false},
                new Answer {AnswerId = 42, AnswerString = "select * from Persons where FirstName=\'a\'", QuestionId = 11, CorrectAnswer = false},
                new Answer {AnswerId = 43, AnswerString = "select * from Persons where FirstName Like \'%a\'", QuestionId = 11,  CorrectAnswer = true},
                new Answer {AnswerId = 44, AnswerString = "select * from Persons where FirstName = \'%a%\'", QuestionId = 11, CorrectAnswer = false},
                new Answer {AnswerId = 45, AnswerString = "Delete from Persons where FirstName = \'Peter\'", QuestionId = 12,CorrectAnswer = true},
                new Answer {AnswerId = 46, AnswerString = "Delete row FirstName = \'Peter\' from Persons", QuestionId = 12, CorrectAnswer = false},
                new Answer {AnswerId = 47, AnswerString = "Delete FirstName = \'Peter\' from Persons", QuestionId = 12, CorrectAnswer = false},
                new Answer {AnswerId = 48, AnswerString = "None of the above", QuestionId = 12, CorrectAnswer = false},
                new Answer {AnswerId = 49, AnswerString = "Sort by" , QuestionId = 13, CorrectAnswer = false},
                new Answer {AnswerId = 50, AnswerString = "Order", QuestionId = 13, CorrectAnswer = false},
                new Answer {AnswerId = 51, AnswerString = "Order by", QuestionId = 13, CorrectAnswer = true},
                new Answer {AnswerId = 52, AnswerString = "Sort", QuestionId = 13, CorrectAnswer = false},
                new Answer {AnswerId = 53, AnswerString = "GET", QuestionId = 14, CorrectAnswer = false},
                new Answer {AnswerId = 54, AnswerString = "FROM", QuestionId = 14, CorrectAnswer = false},
                new Answer {AnswerId = 55, AnswerString = "LIKE", QuestionId = 14, CorrectAnswer = true},
                new Answer {AnswerId = 56, AnswerString = "End", QuestionId = 14, CorrectAnswer = false},
                new Answer {AnswerId = 57, AnswerString = "Select distinct", QuestionId = 15 , CorrectAnswer = true},
                new Answer {AnswerId = 58, AnswerString = "Select unique", QuestionId = 15, CorrectAnswer = false},
                new Answer {AnswerId = 59, AnswerString = "Select Different", QuestionId = 15, CorrectAnswer = false},
                new Answer {AnswerId = 60, AnswerString = "<>=", QuestionId = 15, CorrectAnswer = false},

                 //OOP
                
                new Answer {AnswerId = 61, AnswerString = "class employee : data {}", QuestionId = 16, CorrectAnswer = true},
                new Answer {AnswerId = 62, AnswerString = "class employee implements data {}", QuestionId = 16, CorrectAnswer = false},
                new Answer {AnswerId = 63, AnswerString = "class employee imports data {}", QuestionId = 16, CorrectAnswer = false},
                new Answer {AnswerId = 64, AnswerString = "None of the mentioned", QuestionId = 16, CorrectAnswer = false},
                new Answer {AnswerId = 65, AnswerString = "Public", QuestionId = 17, CorrectAnswer = true },
                new Answer {AnswerId = 66, AnswerString = "Protected", QuestionId = 17, CorrectAnswer = false},
                new Answer {AnswerId = 67, AnswerString = "Private", QuestionId = 17, CorrectAnswer = false},
                new Answer {AnswerId = 68, AnswerString = "All of the above", QuestionId = 17, CorrectAnswer = false},
                new Answer {AnswerId = 69, AnswerString = "upper", QuestionId = 18, CorrectAnswer = false},
                new Answer {AnswerId = 70, AnswerString = "base", QuestionId = 18, CorrectAnswer = true},
                new Answer {AnswerId = 71, AnswerString = "this", QuestionId = 18, CorrectAnswer = false},
                new Answer {AnswerId = 72, AnswerString = "None of the above", QuestionId = 18, CorrectAnswer = false},
                new Answer {AnswerId = 73, AnswerString = "overloads", QuestionId = 19, CorrectAnswer = false},
                new Answer {AnswerId = 74, AnswerString = "overrides", QuestionId = 19, CorrectAnswer = false},
                new Answer {AnswerId = 75, AnswerString = "new", QuestionId = 19, CorrectAnswer = true},
                new Answer {AnswerId = 76, AnswerString = "base", QuestionId = 19, CorrectAnswer = false},
                new Answer {AnswerId =77, AnswerString = "Static class", QuestionId = 20, CorrectAnswer = false},
                new Answer {AnswerId = 78, AnswerString = "Sealed class", QuestionId = 20, CorrectAnswer = false},
                new Answer {AnswerId =79, AnswerString = "Abstract class", QuestionId = 20, CorrectAnswer = true},
                new Answer {AnswerId = 80, AnswerString = "Derived class", QuestionId = 20, CorrectAnswer = false}
            };

            

            modelBuilder.Entity<Answer>().HasData(answers);




            List<ApplicationRole> applicationRole = new List<ApplicationRole>() {
            new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new ApplicationRole { Id = 2, Name = "User", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() }
            };


            modelBuilder.Entity<ApplicationRole>().HasData(applicationRole); 
            
            List<ApplicationUser> applicationUser = new List<ApplicationUser>() {
            new ApplicationUser { Id = 1,  Email = "arturoleksii@gmail.com", UserName= "Arthur", SecurityStamp= Guid.NewGuid().ToString(), NormalizedUserName = "ARTUR", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Ukrainakiev_234")},
            new ApplicationUser { Id = 2, Email = "User@gmail.com", UserName = "User", NormalizedUserName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString(), PasswordHash = BCrypt.Net.BCrypt.HashPassword("User_1") }
            };

            modelBuilder.Entity<ApplicationUser>().HasData(applicationUser);
            /*Task.Run(() =>
            {
                UserManager.AddToRoleAsync(applicationUser.First(),applicationRole.First().Name);
                UserManager.AddToRoleAsync(applicationUser.Last(), applicationRole.Last().Name);

            });*/
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { RoleId = 1, UserId = 1 },
                new UserRole { RoleId = 2, UserId = 2 }
                );
            List<KnowledgeResult> knowledgeResult = new List<KnowledgeResult>() {
                 new KnowledgeResult { UserId= 2, Date = DateTime.Now, KnowledgeResultId = 1 , KnowledgeId = 1, Result = 60  },
             };

            modelBuilder.Entity<KnowledgeResult>().HasData(knowledgeResult);



            List<QuestionResult> questionsResult = new List<QuestionResult>() {
                new QuestionResult {QuestionResultId = 1 , QuestionId = 1, KnowledgeResultId = 1/*, AnswerResult =  new AnswerResult {AnswerId = 3, QuestionResultId = 1, AnswerResultId = 1}*/ },
                new QuestionResult {QuestionResultId = 2,QuestionId = 2,  KnowledgeResultId = 1/*, AnswerResult = new AnswerResult {AnswerId = 5, QuestionResultId = 2, AnswerResultId = 2}*/ },
                new QuestionResult {QuestionResultId = 3, QuestionId = 3, KnowledgeResultId = 1/*, AnswerResult =  new AnswerResult {AnswerId = 10, QuestionResultId = 3, AnswerResultId = 3}*/},
                new QuestionResult {QuestionResultId = 4,QuestionId = 4,  KnowledgeResultId = 1/*, AnswerResult = new AnswerResult {AnswerId = 13, QuestionResultId = 4, AnswerResultId = 4}*/ },
                new QuestionResult {QuestionResultId = 5,QuestionId = 5, KnowledgeResultId = 1/*, AnswerResult = new AnswerResult {AnswerId = 17, QuestionResultId = 5, AnswerResultId = 5}*/ }
            };

            modelBuilder.Entity<QuestionResult>().HasData(questionsResult);
            
           // List<AnswerResult> answersResult = new List<AnswerResult>() {
           //     new AnswerResult {AnswerId = 3/*, QuestionResultId = 1*/, AnswerResultId = 1, QuestionResult = questionsResult.Where(i => i.QuestionResultId == 1).First()},
            //    new AnswerResult {AnswerId = 5/*, QuestionResultId = 2*/, AnswerResultId = 2, QuestionResult = questionsResult.Where(i => i.QuestionResultId == 1).First()},
            //    new AnswerResult {AnswerId = 10/*, QuestionResultId = 3*/, AnswerResultId = 3, QuestionResult = questionsResult.Where(i => i.QuestionResultId == 1).First()},
           //     new AnswerResult {AnswerId = 13/*, QuestionResultId = 4*/, AnswerResultId = 4, QuestionResult = questionsResult.Where(i => i.QuestionResultId == 1).First()},
            //    new AnswerResult {AnswerId = 17/*, QuestionResultId = 5*/, AnswerResultId = 5, QuestionResult = questionsResult.Where(i => i.QuestionResultId == 1).First()}
           //     };

            List<AnswerResult> answersResult = new List<AnswerResult>() {
                new AnswerResult {AnswerId = 3, QuestionResultId = 1, AnswerResultId = 1 },
                new AnswerResult {AnswerId = 5, QuestionResultId = 2, AnswerResultId = 2 },
                new AnswerResult {AnswerId = 10, QuestionResultId = 3, AnswerResultId = 3 },
                new AnswerResult {AnswerId = 13, QuestionResultId = 4, AnswerResultId = 4 },
                new AnswerResult {AnswerId = 17, QuestionResultId = 5, AnswerResultId = 5  }
                };


            modelBuilder.Entity<AnswerResult>().HasData(answersResult);
        }
    }
}
