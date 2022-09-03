using English_Learning.RESX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace English_Learning.Models
{
    public enum StudyMethodsEnum
    {
        Pimsler,
        Leitner
    }
    public static class StudyMethods
    {
        public static List<Method> Methods = new List<Method>()
        {
            //pimsler
            new Method()
            {
                Title = AppResources.PimslerMethodTitle,
                Description = AppResources.PimslerMethodDescription,
                StudyMethodEnum = StudyMethodsEnum.Pimsler,
                Intervals = new TimeSpan[]
                {
                    new TimeSpan(0,30,0),
                    new TimeSpan(8,0,0),
                    new TimeSpan(1,0,0,0),
                    new TimeSpan(3,0,0,0),
                    new TimeSpan(10,0,0,0),
                    new TimeSpan(30,0,0,0),
                    new TimeSpan(90,0,0,0)
                },
                FromLastContact = false
            },
            //leitner
            new Method()
            {
                Title = AppResources.LeitnerMethodTitle,
                Description = AppResources.LeitnerMethodDescription,
                StudyMethodEnum = StudyMethodsEnum.Leitner,
                Intervals = new TimeSpan[]
                {
                    new TimeSpan(1,0,0),
                    new TimeSpan(2,0,0),
                    new TimeSpan(7,0,0,0),
                    new TimeSpan(14,0,0,0),
                    new TimeSpan(30,0,0,0)
                },
                FromLastContact = false
            }
        };

        public static List<string> GetMethodNames()
        {
            List<string> names = new List<string>();
            foreach (Method method in Methods)
            {
                names.Add(method.Title);
            };
            return names;

            //nullref
            //return (List<string>)from method in Methods
            //                     select method.Title;


        }
        public static Method GetDefaultMethod()
        {
            string methodName = Preferences.Get("default_method", StudyMethods.Methods[0].Title);
            return Methods.FirstOrDefault(m => m.Title == methodName);
        }
        public static Method GetStudyMethodFromEnum(StudyMethodsEnum studyMethodsEnum)
        {
            switch (studyMethodsEnum)
            {
                case StudyMethodsEnum.Pimsler:
                    return Methods[0];
                case StudyMethodsEnum.Leitner:
                    return Methods[1];
                default:
                    return GetDefaultMethod();
            }
        }
        public static Method GetStudyMethodFromString(string methodName)
        {
            Method method = Methods.FirstOrDefault(m => m.Title == methodName);

            if (method == null)
                return GetDefaultMethod();

            return method;
        }
        public static string GetMethodNameFormEnum(StudyMethodsEnum studyMethod)
        {
            switch (studyMethod)
            {
                case StudyMethodsEnum.Pimsler:
                    return Methods[0].Title;
                case StudyMethodsEnum.Leitner:
                    return Methods[1].Title;
                default:
                    return "";
            }
        }
        public static StudyMethodsEnum GetMethodEnumFromString(string methodName)
        {
            Method method = Methods.FirstOrDefault(m => m.Title == methodName);
            
            if (method == null)
                return GetDefaultMethod().StudyMethodEnum;

            return method.StudyMethodEnum;
        }
    }
}
