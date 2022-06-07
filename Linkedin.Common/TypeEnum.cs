using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.Common
{
  public class TypeEnum
    {
        public enum UserStatus
        {

            [Display(Name = "Submit")]
            Submit = 1,

            [Display(Name = "InProgress")]
            InProgress = 2,

            [Display(Name = "Deleted")]
            Deleted = 3
        }

        public enum ScheduleStatus
        {

            [Display(Name = "Submit")]
            Submit = 1,

            [Display(Name = "Deleted")]
            Deleted = 2
        }

        public enum ActivityStatus
        {

            [Display(Name = "Submit")]
            Submit = 1,

            [Display(Name = "ReadyToLike")]
            ReadyToLike = 2,

            [Display(Name = "Liked")]
            Liked = 3
        }

        public enum RequestStatus
        {
            [Display(Name = "Submit")]
            Submit = 1,

            [Display(Name = "Scheduled")]
            Scheduled = 2,

            [Display(Name = "Requested")]
            Requested = 3,

            [Display(Name = "Rejected")]
            Rejected = 4,

            [Display(Name = "Accepted")]
            Accepted = 5,

            [Display(Name = "Canceled")]
            Canceled = 6
        }
    }
}
