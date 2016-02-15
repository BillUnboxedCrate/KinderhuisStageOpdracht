using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KinderhuisStageOpdracht.Models.DAL
{
    public class ProjectInitializer : DropCreateDatabaseAlways<ProjectContext>
    {
    }
}