using System.Web.Http.ModelBinding;
using System.Linq;
using System.Collections.Generic;
using System;

namespace NewcomersNetworkAPI.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static ModelStateDictionary WithoutFormName(this ModelStateDictionary modelstate)
        {
            ModelStateDictionary newMState = new ModelStateDictionary(modelstate);
            newMState.Clear();

            foreach (var key in modelstate.Keys)
            {
                var errors = String.Join(", ", modelstate[key].Errors
                                                    .Select(e => e.ErrorMessage)
                                                    .ToList() );

                newMState.AddModelError(NormalizeKeyName(key.ToString()), errors);
            }
            
            return newMState;
        }

        private static string NormalizeKeyName(string key)
        {
            string newKey = key;
            try
            {
                newKey = key.Split(new char[] { '.' }, StringSplitOptions.None)[1] ?? key;
            }
            catch (Exception e)
            {
                // No problem
            }
            return newKey;
        }

    }
}