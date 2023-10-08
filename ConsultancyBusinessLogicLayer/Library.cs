using System;
using System.Collections.Generic;
using System.Linq;
using static ConsultancyBusinessLogicLayer.Library;

namespace ConsultancyBusinessLogicLayer
{
    public enum UserType
    {
        JobSeeker,
        Employer
    }

    public class Library
    {
        public class Register
        {
            public void JobSeekerRegister(JobSeekerOperations jobSeekerOperations)
            {
                Console.WriteLine("Job Seeker Registration:");
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                Console.Write("Enter your date of birth (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                {
                    int age = DateTime.Now.Year - dateOfBirth.Year;

                    if (age < 18)
                    {
                        Console.WriteLine("You are not eligible for a job due to age.");
                        return;
                    }

                    Console.Write("Are you a fresher? (yes/no): ");
                    bool isFresher = Console.ReadLine().ToLower() == "yes";

                    Personal personalDetails = new Personal
                    {
                        Name = name,
                        DateOfBirth = dateOfBirth,
                        IsFresher = isFresher,
                        ExpertField = ""
                    };

                    if (!isFresher)
                    {
                        Console.Write("Enter your company name: ");
                        string companyName = Console.ReadLine();

                        Console.Write("Enter your years of experience: ");
                        if (int.TryParse(Console.ReadLine(), out int experienceYears))
                        {
                            Console.WriteLine("Enter your skills (comma-separated): ");
                            string[] skills = Console.ReadLine().Split(',');

                            Professional professionalDetails = new Professional
                            {
                                Name = name,
                                IsFresher = false,
                                CompanyName = companyName,
                                ExperienceYears = experienceYears,
                                Skills = skills
                            };

                            personalDetails.ProfessionalDetails = professionalDetails;
                        }
                        else
                        {
                            Console.WriteLine("Invalid experience years input.");
                        }
                    }

                    jobSeekerOperations.RegisterJobSeeker(personalDetails);

                    Console.WriteLine("Job seeker registration successful!");
                }
                else
                {
                    Console.WriteLine("You are not eligible or provided an invalid date of birth input.");
                }
            }

            public void EmployerRegister(EmployerOperations employerOperations)
            {
                Console.WriteLine("Employer Registration:");
                Console.Write("Enter your company name: ");
                string companyName = Console.ReadLine();

                Console.Write("Enter your contact email: ");
                string contactEmail = Console.ReadLine();

                Console.Write("Enter your industry: ");
                string industry = Console.ReadLine();

                Employer employer = new Employer
                {
                    CompanyName = companyName,
                    ContactEmail = contactEmail,
                    Industry = industry
                };

                employerOperations.RegisterEmployer(employer);

                Console.WriteLine("Employer registration successful!");
            }
        }

        public class Personal
        {
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
            public int Age => DateTime.Now.Year - DateOfBirth.Year;
            public bool IsFresher { get; set; }
            public Professional ProfessionalDetails { get; set; }
            public Academic AcademicDetails { get; set; }
            public string ExpertField { get; set; }
            public bool IsEligibleForJob => Age > 18;
        }

        public class Professional
        {
            public string Name { get; set; }
            public bool IsFresher { get; set; }
            public string CompanyName { get; set; }
            public int ExperienceYears { get; set; }
            public string[] Skills { get; set; }
        }

        public class Academic
        {
            public int GraduationMarks { get; set; }
            public string[] Skills { get; set; }
        }

        internal class Employer
        {
            public string CompanyName { get; internal set; }
            public string ContactEmail { get; internal set; }
            public string Industry { get; internal set; }
        }
    }

    public class Employer
    {
        public string CompanyName { get; set; }
        public string ContactEmail { get; set; }
        public string Industry { get; set; }
    }

    public class JobSeekerOperations
    {
        private List<Personal> jobSeekers = new List<Personal>();

        public void RegisterJobSeeker(Personal jobSeeker)
        {
            jobSeekers.Add(jobSeeker);
        }

        public Personal[] SearchJob(string industry, string specialization)
        {
            return jobSeekers
                .Where(jobSeeker => jobSeeker.ExpertField.Equals(specialization, StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }
    }

    public class EmployerOperations
    {
        private List<Employer> employers = new List<Employer>();

        public void RegisterEmployer(Employer employer)
        {
            employers.Add(employer);
        }

        internal void RegisterEmployer(Library.Employer employer)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        public static void Main()
        {
            JobSeekerOperations jobSeekerOperations = new JobSeekerOperations();
            EmployerOperations employerOperations = new EmployerOperations();
            Library.Register register = new Library.Register();

            Console.WriteLine("Select user type (JobSeeker or Employer):");
            UserType userType = (UserType)Enum.Parse(typeof(UserType), Console.ReadLine(), true);

            if (userType == UserType.JobSeeker)
            {
                register.JobSeekerRegister(jobSeekerOperations);
            }
            else if (userType == UserType.Employer)
            {
                register.EmployerRegister(employerOperations);
            }

            // Implement job searching and other operations here based on user type.
        }
    }
}
