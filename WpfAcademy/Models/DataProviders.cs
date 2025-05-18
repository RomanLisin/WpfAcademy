using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using CacheLibrary;
using System.Windows;




namespace WpfAcademy.Models
{
	
	public static class DataProviders
	{
		public static DataSet LoadData()
		{
			Cache cache = new Cache(ConfigurationManager.ConnectionStrings["VPD_311_Import"].ConnectionString);
			if (cache != null) { MessageBox.Show("✅ Подключение установлено"); } else { MessageBox.Show("Не удалось подключиться"); }
				cache.AddTable("Students", "stud_id,last_name,first_name,[group]");
			cache.AddTable("Groups", "group_id,group_name,direction");
			cache.AddTable("Directions", "direction_id,direction_name");
			cache.AddTable("Disciplines", "discipline_id,discipline_name,number_of_lessons");
			cache.AddTable("Teachers", "teacher_id,last_name,first_name,middle_name,birth_date,work_since,rate");

			cache.AddRelation("StudentsGroups", "Students,[group]", "Groups,group_id");
			cache.AddRelation("GroupsDirections", "Groups,direction", "Directions,direction_id");
			//cache.AddRelation("")

			return cache.Set;

		}
	}
}
