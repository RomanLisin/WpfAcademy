using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CacheLibrary;

namespace WpfAcademy.Models
{
	
	public  class DataProviders
	{
        private readonly string CONNECTION_STRING="";

		private Cache cache;
       

        public DataProviders()
        {
            CONNECTION_STRING = ConfigurationManager.ConnectionStrings["VPD_311_Import"].ConnectionString;
            cache = new Cache(CONNECTION_STRING);

            // Инициализация таблиц и их колонок
            InitializeCacheTables();
        }

        private void InitializeCacheTables()
        {
            // Добавляем таблицы с нужными колонками
            cache.AddTable("Directions", "direction_id,direction_name");
            cache.AddTable("Groups", "group_id,group_name,direction");
            cache.AddTable("Students", "stud_id,last_name,first_name,[group]");

            // Устанавливаем связи между таблицами
            cache.AddRelation("GroupsDirections", "Groups,direction", "Directions,direction_id");
            cache.AddRelation("StudentsGroups", "Students,group", "Groups,group_id");
        }

        public DataSet LoadData()
        {
            // Возвращаем DataSet из кэша
            return cache.Set;
        }

        public void CheckForUpdates()
        {
            // Проверяем изменения в базе данных
            cache.CheckChangesDataBase();
        }

        public void PrintTable(string tableName)
        {
            // Выводим содержимое таблицы (для отладки)
            cache.Print(tableName);
        }
    }
}
