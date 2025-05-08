using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using WpfAcademy.Models;

namespace WpfAcademy.ViewModels
{
	public class StudentInfo
	{
		public int StudId { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public int? GroupId { get; set; }
		public string GroupName { get; set; }
		public string DirectionName { get; set; }
	}

	public class DirectionItem
	{
		public int DirectionId { get; set; }
		public string DirectionName { get; set; }

		public override string ToString()
		{
			return DirectionName;
		}
	}
	public class GroupItem
	{
		public int GroupId { get; set; }
		public string GroupName { get; set; }

		public override string ToString()
		{
			return GroupName;
		}
	}
	public class MainViewModel : INotifyPropertyChanged
	{


		public ObservableCollection<StudentInfo> Students { get; set; } = new ObservableCollection<StudentInfo>();
		public ObservableCollection<DirectionItem> Directions { get; set; } = new ObservableCollection<DirectionItem>();
		public ObservableCollection<GroupItem> Groups { get; set; } = new ObservableCollection<GroupItem>();


		private DirectionItem _selectedDirection;
		public DirectionItem SelectedDirection
		{
			get { return _selectedDirection; }
			set
			{
				if (_selectedDirection != value)
				{
					_selectedDirection = value;
					OnPropertyChanged(nameof(SelectedDirection));
					LoadGroups();
				}
			}
		}

		private GroupItem _selectedGroup;
		public GroupItem SelectedGroup
		{
			get { return _selectedGroup; }
			set
			{
				if (_selectedGroup != value)
				{
					_selectedGroup = value;
					OnPropertyChanged(nameof(SelectedGroup));
					LoadStudents();
				}
			}
		}


		private DataSet _data;

		public MainViewModel()
		{
			Students = new ObservableCollection<StudentInfo>();
			_data = DataProviders.LoadData();

			LoadDirections();
		}

		private void LoadDirections()
		{
			Directions.Clear();
			Directions.Add(new DirectionItem { DirectionId = 0, DirectionName = "Все направления" });

			foreach (DataRow row in _data.Tables["Directions"].Rows)
			{
				Directions.Add(new DirectionItem
				{
					DirectionId = row.Field<int>("direction_id"),
					DirectionName = row.Field<string>("direction_name")
				});
			}

			SelectedDirection = Directions[0];
		}

		private void LoadGroups()
		{
			Groups.Clear();

			Groups.Add(new GroupItem { GroupId = 0, GroupName = "Все группы" });

			foreach (DataRow row in _data.Tables["Groups"].Rows)
			{
				int? directionId = row.Field<int?>("direction");

				if (SelectedDirection.DirectionId == 0 || directionId == SelectedDirection.DirectionId)
				{
					Groups.Add(new GroupItem
					{
						GroupId = row.Field<int>("group_id"),
						GroupName = row.Field<string>("group_name")
					});
				}
			}

			SelectedGroup = Groups[0];
		}
		private void LoadStudents()
		{
			if (SelectedDirection == null || SelectedGroup == null)
				return;

			Students.Clear();

			foreach (DataRow student in _data.Tables["Students"].Rows)
			{
				int? studentGroupId = student.Field<int?>("group");

				DataRow groupRow = _data.Tables["Groups"].AsEnumerable()
					.FirstOrDefault(g => g.Field<int>("group_id") == studentGroupId);

				if (groupRow == null)
					continue;

				int? groupDirectionId = groupRow.Field<int?>("direction");

				// Пропускаем, если не совпадает выбранное направление
				if (SelectedDirection.DirectionId != 0 && groupDirectionId != SelectedDirection.DirectionId)
					continue;

				// Пропускаем, если не совпадает выбранная группа
				if (SelectedGroup.GroupId != 0 && studentGroupId != SelectedGroup.GroupId)
					continue;

				DataRow directionRow = _data.Tables["Directions"].AsEnumerable()
					.FirstOrDefault(d => d.Field<int>("direction_id") == groupDirectionId);

				StudentInfo studentInfo = new StudentInfo
				{
					StudId = student.Field<int>("stud_id"),
					LastName = student.Field<string>("last_name"),
					FirstName = student.Field<string>("first_name"),
					GroupId = studentGroupId,
					GroupName = groupRow?.Field<string>("group_name") ?? "",
					DirectionName = directionRow?.Field<string>("direction_name") ?? ""
				};

				Students.Add(studentInfo);
			}

		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}

}
