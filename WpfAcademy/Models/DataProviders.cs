using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Runtime.InteropServices;
//using System.Data.SqlClient;
using System.Configuration;   // for using ConfigurationManager and add throw Nuget manager  ConfigurationManager package
//using System.ComponentModel;

using CacheLibrary;

namespace WpfAcademy.Models
{
	
	public static class DataProviders
	{
		public static DataSet LoadData()
		{

			Cache cache;
			cache = new Cache(ConfigurationManager.ConnectionStrings["VPD_311_Import"].ConnectionString);
			// Здесь можно подключиться к БД или создать тестовые таблицы
			//DataSet ds = new DataSet();

			//DataTable students = new DataTable("Students");
			//DataTable groups = new DataTable("Groups");
			//DataTable directions = new DataTable("Directions");

			cache.AddTable("Students", "stud_id,last_name,first_name,middle_name,group");
			cache.AddTable("Directions", "direction_id,direction_name");
			cache.AddTable("Groups", "group_id,group_name,direction");
			//cache.AddRelation("StudentsGroups", "Students,group", "Groups,group_id");
			//cache.AddRelation("GroupsDirections", "Groups,direction", "Directions,direction_id");
			//students.Columns.Add("stud_id", typeof(int));
			//students.Columns.Add("last_name", typeof(string));
			//students.Columns.Add("first_name", typeof(string));
			//students.Columns.Add("group", typeof(int));
			//students.Rows.Add(1, "Иванов", "Иван", 1);
			//students.Rows.Add(2, "Петров", "Петр", 1);
			//students.Rows.Add(3, "Сидоров", "Геннадий", 2);
			//students.Rows.Add(4, "Василий", "Пупкин", 3);
			//ds.Tables.Add(students);

			//groups.Columns.Add("group_id", typeof(int));
			//groups.Columns.Add("group_name", typeof(string));
			//groups.Columns.Add("direction", typeof(int));
			//groups.Rows.Add(1, "Группа А", 1);
			//groups.Rows.Add(2,"Группа Б",  1);
			//groups.Rows.Add(3, "Группа В", 2);
			//ds.Tables.Add(groups);

			//directions.Columns.Add("direction_id", typeof(int));
			//directions.Columns.Add("direction_name", typeof(string));
			//directions.Rows.Add(1, "Направление X");
			//directions.Rows.Add(2, "Направление С");
			//ds.Tables.Add(directions);

			return cache.Set;
		}
	}
}
