using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task.Models;

namespace Task.Models
{
    public class ApplicationProfile :AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            //Define automapper map 
            CreateMap<Accounts, TaskDTO>();
            CreateMap<TaskDTO, Accounts>();
          

        }
    }
}