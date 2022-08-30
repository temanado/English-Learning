using English_Learning.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace English_Learning.Services
{
    public class ParametersDataStore : IDataStore<Parameter>
    {
        List<Parameter> parameters;
        public ParametersDataStore()
        {
            parameters = new List<Parameter>()
            {
                new Parameter { Id = "DefaultStudyMethod", Name = "Default Study Method", Value = "Pimsler", Type = Type.GetType("Sistem.String") },
                new Parameter { Id = "Notification", Name = "Notification Type", Value = "Push", Type = Type.GetType("Sistem.String") },
                new Parameter { Id = "AppActivityStartDate", Name = "App Activity Start Date", Value = DateTime.MinValue, Type = Type.GetType("System.DateTime") },
                new Parameter { Id = "AppActivityEndDate", Name = "App Activity End Date", Value = DateTime.MaxValue, Type = Type.GetType("System.DateTime") },
                new Parameter { Id = "AppLanguage", Name = "Language", Value = new CultureInfo("en-US"), Type = Type.GetType("System.Globalization.CultureInfo") },
                new Parameter { Id = "TranslationIsVisible", Name = "Translation Is Visible", Value = false, Type = Type.GetType("System.Boolean") }
            };
        }
        public async Task<bool> AddItemAsync(Parameter parameter)
        {
            parameters.Add(parameter);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Parameter parameter)
        {
            var oldParameter = parameters.Where((Parameter arg) => arg.Id == parameter.Id).FirstOrDefault();
            parameters.Remove(oldParameter);
            parameters.Add(parameter);

            return await Task.FromResult(true);
        }
        public async Task<Parameter> GetItemAsync(string id)
        {
            return await Task.FromResult(parameters.FirstOrDefault(s => s.Id == id));
        }
        public async Task<bool> DeleteItemAsync(string id)
        {
            Parameter findedParameter = parameters.Where((Parameter arg) => arg.Id == id).FirstOrDefault();

            if (findedParameter != null)
                parameters.Remove(findedParameter);

            return await Task.FromResult(true);
        }
        public async Task<IEnumerable<Parameter>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(parameters);
        }
    }
}