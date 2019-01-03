using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;
using System.IO.MemoryMappedFiles;
using System.Text.RegularExpressions;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            SearchJobsViewModel jobsViewModel = new SearchJobsViewModel();
            jobsViewModel.Jobs = jobData.Find(id);
            return View( "Index", jobsViewModel);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Location = jobData.Locations.Find(Convert.ToInt32(newJobViewModel.Location)),
                    Employer = jobData.Employers.Find(Convert.ToInt32(newJobViewModel.EmployerID)),
                    PositionType = jobData.PositionTypes.Find(Convert.ToInt32(newJobViewModel.PositionType)),
                    CoreCompetency = jobData.CoreCompetencies.Find(Convert.ToInt32(newJobViewModel.Skill))

                };
                jobData.Jobs.Add(newJob);
                
                //return View(newJobViewModel);
               
                return Redirect("/Job?id=99");
            }
            return View(newJobViewModel);

        }
        
    }
}
