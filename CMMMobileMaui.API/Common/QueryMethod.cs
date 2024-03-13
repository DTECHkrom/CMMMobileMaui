using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CMMMobileMaui.API.Common
{
    public class QueryMethod
    {
        public string Name { get; private set; }
        private object jsonObj;

        public QueryMethod([CallerMemberName] in string name = "")
        {
            this.Name = name;
        }

        public QueryMethod(MethodBase method)
        {
            this.Name = method.Name;
        }

        public List<QueryParam> QueryParams { get; set; }

        public override string ToString() =>
            $"{Name}" + ((QueryParams == null || QueryParams.Count == 0) ? string.Empty
            : "?" + string.Join("&", QueryParams)
            );

        public QueryMethod SetJsonObject(object obj)
        {
            jsonObj = obj;
            return this;
        }

        public string GetJsobStringFromObject() =>
            this.jsonObj == null ? string.Empty
            : QueryParams == null || QueryParams.Count > 0 ? JsonConvert.SerializeObject(jsonObj, Formatting.Indented)
            : string.Empty;

        public QueryMethod AddParam(QueryParam param)
        {
            if (QueryParams == null)
            {
                QueryParams = new List<QueryParam>();
            }

            QueryParams.Add(param);

            return this;
        }

        public QueryMethod AddParams<T>(T param)
        {
            if (QueryParams == null)
            {
                QueryParams = new List<QueryParam>();
            }

            var properties = typeof(T).GetProperties();

            foreach(var property in properties.Where(tt=> tt.GetValue(param) != null))
            {
                if (property.GetCustomAttribute(typeof(JsonIgnoreAttribute)) != null)
                    continue;

                if(!property.PropertyType.IsGenericType)
                {
                    QueryParams.Add(new QueryParam
                    {
                        Name = property.Name
                                ,
                        Value = property.GetValue(param).ToString()
                    });
                }
                else
                {
                    var genericType = property.PropertyType.GetGenericTypeDefinition();
                    var tempList = property.GetValue(param) as IList;

                    if (tempList != null)
                    {
                        List<QueryParam> tempParamsList = new List<QueryParam>();

                        foreach (var item in tempList)
                        {
                            QueryParams.Add(new QueryParam
                            {
                                Name = property.Name
                                ,
                                Value = item.ToString()
                            }); ;
                        }
                    }
                    else if(Nullable.GetUnderlyingType(property.PropertyType) != null)
                    {
                        QueryParams.Add(new QueryParam
                        {
                            Name = property.Name
                                ,
                            Value = property.GetValue(param).ToString()
                        });
                    }
                }
            }

            //QueryParams.AddRange(typeof(T).GetProperties()
            //    .Where(property => property.GetValue(param) != null && !property.PropertyType.IsGenericType)
            //    .Select(property => 
            //    new QueryParam() { Name = property.Name
            //    , Value = property.GetValue(param).ToString() })
            //    );

            //QueryParams.AddRange(typeof(T).GetProperties()
            //    .Where(property => property.GetValue(param) != null && property.PropertyType.IsGenericType)
            //    .SelectMany(property =>
            //    {
            //        var genericType = property.PropertyType.GetGenericTypeDefinition();
            //        var tempList = property.GetValue(param) as IList;

            //        if(tempList != null)
            //        {
            //            List<QueryParam> tempParamsList = new List<QueryParam>();

            //            foreach(var item in tempList)
            //            {
            //                tempParamsList.Add(new QueryParam
            //                {
            //                    Name = property.Name
            //                    , Value = item.ToString()
            //                });;
            //            }

            //            return tempParamsList;
            //        }

            //        return Enumerable.Empty<QueryParam>();
            //    }));

            return this;
        }
    }
}
