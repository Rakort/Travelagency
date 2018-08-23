using System;
using System.Windows.Input;
using SQLite;

namespace Travelagency.Model
{
    public class Table
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        public ICommand SaveCommand => new SimpleCommand(() =>Save());
        public int Save() 
        {
            //if (Sql.Contains(this))
            if (this.Id == 0)
                    return Sql.Add(this);
            else
                return Sql.Update(this);
        }

        public void Delete()
        {
            Sql.Delete(this);
        }
        

    }
    /// <summary>
    /// Заявки
    /// </summary>
    public class Order : Table
    {
        /// <summary>
        /// Заявление(true) или обращение(false)
        /// </summary>
        public bool IsOrder { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Количество взрослых
        /// </summary>
        public int NumberAdults { get; set; }
        /// <summary>
        /// Количество детей
        /// </summary>
        public int NumberChildren { get; set; }
        /// <summary>
        /// Питание
        /// </summary>
        public string Food { get; set; }
        /// <summary>
        /// Размещение
        /// </summary>
        public string Accommodation { get; set; }
        /// <summary>
        /// Количество ночей
        /// </summary>
        public int NumberNights { get; set; }
        /// <summary>
        /// Туроператор
        /// </summary>
        public string Touroperator { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Fio => $"{LastName} {FirstName} {SecondName}";
    }

    /// <summary>
    /// Тур
    /// </summary>
    public class Tour : Table
    {
        /// <summary>
        /// Туроператор
        /// </summary>
        public string Touroperator { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Отель
        /// </summary>
        public string Hotel { get; set; }
        /// <summary>
        /// Размещение
        /// </summary>
        public string Accommodation { get; set; }
        /// <summary>
        ///Питание
        /// </summary>
        public string Food { get; set; }
        /// <summary>
        /// Начальная дата
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Конечная дата
        /// </summary>
        public DateTime? EndDate { get; set; }
    }

    


}
