using System;
using System.Collections.Generic;
using System.Text;

namespace English_Learning.Models
{
    public class GlobalSetting
    {
        private string _defaultStudyMethod;
        private string _notification;
        private DateTime _appActivityStartDate;
        private DateTime _appActivityEndDate;

        public string DefaultStudyMethod
        {
            get { return _defaultStudyMethod; }
            set
            {
                _defaultStudyMethod = value;
            }
        }
        public string Notification
        {
            get { return _notification; }
            set
            {
                _notification = value;
            }
        }
        public DateTime AppActivityStartDate
        {
            get
            {
                return _appActivityStartDate;
            }
            set
            {
                _appActivityStartDate = value;
            }
        }
        public DateTime AppActivityEndDate
        {
            get
            {
                return _appActivityEndDate;
            }
            set
            {
                _appActivityEndDate = value;
            }
        }
    }
}
