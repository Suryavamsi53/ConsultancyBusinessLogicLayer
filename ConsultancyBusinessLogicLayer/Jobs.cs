using System;
using ConsultancyBusinessLogicLayer;
namespace ConsultancyBusinessLogicLayer
{

    public class JobRequirement
    {
        // Define properties and methods for the JobRequirement class as needed.
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // Other properties and methods...
    }
    public class Jobs
    {
        private JobRequirement[] jobRequirements = new JobRequirement[100]; // You can choose an appropriate initial size.

        private int nextIndex = 0; // Keeps track of the next available index in the array.

        public void PostRequirement(JobRequirement requirement)
        {
            if (nextIndex < jobRequirements.Length)
            {
                jobRequirements[nextIndex] = requirement;
                nextIndex++;
            }
            else
            {
                // Handle the case where the array is full.
                Console.WriteLine("Job requirements array is full. Cannot add more.");
            }
        }

        public void EditRequirement(int requirementId, JobRequirement updatedRequirement)
        {
            // Implement editing logic based on the requirementId.
            // You should search for the requirement by its ID in the array and update it.
            for (int i = 0; i < nextIndex; i++)
            {
                if (jobRequirements[i] != null && jobRequirements[i].Id == requirementId)
                {
                    jobRequirements[i] = updatedRequirement;
                    break; // Assuming each ID is unique; you can break once found.
                }
            }
        }

        public void DeleteRequirement(int requirementId)
        {
            // Implement deletion logic based on the requirementId.
            // You should search for the requirement by its ID in the array and remove it.
            for (int i = 0; i < nextIndex; i++)
            {
                if (jobRequirements[i] != null && jobRequirements[i].Id == requirementId)
                {
                    // Shift elements to fill the gap created by removing the requirement.
                    for (int j = i; j < nextIndex - 1; j++)
                    {
                        jobRequirements[j] = jobRequirements[j + 1];
                    }
                    jobRequirements[nextIndex - 1] = null;
                    nextIndex--;
                    break; // Assuming each ID is unique; you can break once found.
                }
            }
        }
    }
}