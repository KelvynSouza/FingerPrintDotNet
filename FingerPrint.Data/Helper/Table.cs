﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FingerPrint.Data.Helper
{
    public static class Table
    {
        public static List<T> ToListModel<T>(this DataTable dt)
        {
            List<string> columns = (from DataColumn dc in dt.Columns select dc.ColumnName).ToList();

            var fields = typeof(T).GetFields();
            var properties = typeof(T).GetProperties();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields.Where(fieldInfo => columns.Contains(fieldInfo.Name)))
                {
                    fieldInfo.SetValue(ob, !dr.IsNull(fieldInfo.Name) ? dr[fieldInfo.Name] : fieldInfo.FieldType.IsValueType ? Activator.CreateInstance(fieldInfo.FieldType) : null);
                }

                foreach (var propertyInfo in properties.Where(propertyInfo => columns.Contains(propertyInfo.Name)))
                {
                    propertyInfo.SetValue(ob, !dr.IsNull(propertyInfo.Name) ? dr[propertyInfo.Name] : propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null);
                }

                lst.Add(ob);
            }

            return lst;
        }

        public static T ToModel<T>(this DataTable dt)
        {
            List<string> columns = (from DataColumn dc in dt.Columns select dc.ColumnName).ToList();

            var fields = typeof(T).GetFields();
            var properties = typeof(T).GetProperties();



            DataRow dr = dt.Rows[0];

            var ob = Activator.CreateInstance<T>();

            foreach (var fieldInfo in fields.Where(fieldInfo => columns.Contains(fieldInfo.Name)))
            {
                fieldInfo.SetValue(ob, !dr.IsNull(fieldInfo.Name) ? dr[fieldInfo.Name] : fieldInfo.FieldType.IsValueType ? Activator.CreateInstance(fieldInfo.FieldType) : null);
            }

            foreach (var propertyInfo in properties.Where(propertyInfo => columns.Contains(propertyInfo.Name)))
            {
                propertyInfo.SetValue(ob, !dr.IsNull(propertyInfo.Name) ? dr[propertyInfo.Name] : propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null);
            }

            return ob;


        }
    }
}
