using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsultancyBusinessLogicLayer
{
    public class JobSeekerOperation
    {
        // ... Other existing code ...

        public class Jobs
        {
            private List<JobRequirement> jobRequirements = new List<JobRequirement>();

            public void PostRequirement(Employer employer, JobRequirement requirement)
            {
                // You can associate the requirement with the employer
                requirement.EmployerId = employer.Id;
                jobRequirements.Add(requirement);
            }

            public void EditRequirement(Employer employer, int requirementId, JobRequirement updatedRequirement)
            {
                // Find the requirement by ID and ensure it belongs to the employer
                var requirement = jobRequirements.FirstOrDefault(r => r.Id == requirementId && r.EmployerId == employer.Id);
                if (requirement != null)
                {
                    // Update the requirement details
                    requirement.Title = updatedRequirement.Title;
                    requirement.Description = updatedRequirement.Description;
                    requirement.SkillsRequired = updatedRequirement.SkillsRequired;
                }
                else
                {
                    Console.WriteLine("Requirement not found or does not belong to this employer.");
                }
            }

            public void DeleteRequirement(Employer employer, int requirementId)
            {
                // Find the requirement by ID and ensure it belongs to the employer
                var requirement = jobRequirements.FirstOrDefault(r => r.Id == requirementId && r.EmployerId == employer.Id);
                if (requirement != null)
                {
                    jobRequirements.Remove(requirement);
                }
                else
                {
                    Console.WriteLine("Requirement not found or does not belong to this employer.");
                }
            }

            public List<JobRequirement> SearchJobs(string keyword)
            {
                // Search for job requirements that match the keyword in title or description
                return jobRequirements
    .Where(r => r.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                r.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
    .ToList();

            }
        }

        public class JobRequirement
        {
            public int Id { get; set; }
            public int EmployerId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string SkillsRequired { get; set; }
        }

        public class Employer
        {
            public int Id { get; set; } // Add an identifier for the employer
            public string CompanyName { get; set; }
            public string ContactEmail { get; set; }
            public string Industry { get; set; }
        }

        // ... Other existing code ...
    }

    // ... Other existing code ...
}
