using System;
using System.Data.SqlClient;

namespace Task_11
{
    class SqlHelper
    {
        private SqlConnection SqlConnection;
        private SqlCommand sqlCmd;
        public SqlHelper(string connetctionString)
        {
            SqlConnection = new SqlConnection(connetctionString);
        }

        public bool Connect()
        {
            SqlConnection.Open();
            if(SqlConnection.State == System.Data.ConnectionState.Open)
                return true;
            else
                return false;
        }
        public void Close()
        {
            SqlConnection.Close();
        }

        public void Insert(params string[] content)
        {
            string cmd = $"insert into Person(LastName, FirstName, MiddleName, BirthDate) values('{content[0]}', '{content[1]}', '{content[2]}', '{content[3]}')";
            sqlCmd = new SqlCommand(cmd, SqlConnection);
            if(this.Connect())
            {
                sqlCmd.ExecuteNonQuery();
                this.Close();
                Console.WriteLine("Данные добавлены");
            }
            else
                Console.WriteLine("Ошибка соединения");
        }

        public void SelectAll()
        {
            string cmd = "select * from Person";
            sqlCmd = new SqlCommand(cmd, SqlConnection);
            if(this.Connect())
            {
                sqlCmd.ExecuteNonQuery();
                this.Close();
                Console.WriteLine("Все элементы были выбраны.");
            }
            else
                Console.WriteLine("Ошибка соединения");
        }

        public void SelectByID(int ID)
        {
            string cmd = $"select LastName, FirstName, MiddleName, BirthDate from Person where ID = {ID}";
            sqlCmd = new SqlCommand(cmd, SqlConnection);
            if(this.Connect())
            {
                sqlCmd.ExecuteNonQuery();
                this.Close();
                Console.WriteLine($"Данные выбраны по {ID}");
            }
            else
                Console.WriteLine("Ошибка соединения");
        }

        public void Update(int ID, params string[] data)
        {
            string cmd = $"update Person set LastName = '{data[0]}', FirstName = '{data[1]}', MiddleName = '{data[2]}', BirthDate = '{data[3]}' where ID = {ID}";
            sqlCmd = new SqlCommand(cmd, SqlConnection);
            if(this.Connect())
            {
                sqlCmd.ExecuteNonQuery();
                this.Close();
                Console.WriteLine("Данные обновленны");
            }
            else
                Console.WriteLine("Ошибка соединения");
        }

        public void Delete(int ID)
        {
            string cmd = $"delete Person where ID = {ID}";
            sqlCmd = new SqlCommand(cmd, SqlConnection);
            if(this.Connect())
            {
                sqlCmd.ExecuteNonQuery();
                this.Close();
                Console.WriteLine("Данные удаленны");
            }
            else
                Console.WriteLine("Ошибка соединения");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string menu = "\n1.\tДобавить\n"
                        + "2.\tУдалить\n"
                        + "3.\tВыбрать все\n"
                        + "4.\tВыбрать по ID\n"
                        + "5.\tОбновить\n"
                        + "0.\tВыход\n";
            Console.WriteLine("Здравствуйте! Выберите что вы хотите сделать: " + menu);
            string cmd = string.Empty;
            SqlHelper sqlHelp = new SqlHelper(@"Data Source=localhost;Initial Catalog=Masud;Integrated Security=true;");
            while(cmd != "0")
            {
                cmd = Console.ReadLine();
                if(cmd == "1")
                {
                    Console.WriteLine("Введите имя: ");
                    string s1 = Console.ReadLine();
                    Console.WriteLine("Введите фамилию: ");
                    string s2 = Console.ReadLine();
                    Console.WriteLine("Введите отчество: ");
                    string s3 = Console.ReadLine();
                    Console.WriteLine("Введите дату рождения: ");
                    string s4 = Console.ReadLine();
                    sqlHelp.Insert(s1, s2, s3, s4);
                }
                else if(cmd == "2")
                {
                    Console.WriteLine("Введите ID: ");
                    int ID = int.Parse(Console.ReadLine());
                    sqlHelp.Delete(ID);
                }
                else if(cmd == "3")
                {
                    sqlHelp.SelectAll();
                }
                else if(cmd == "4")
                {
                    Console.WriteLine("Введите ID: ");
                    int ID = int.Parse(Console.ReadLine());
                    sqlHelp.SelectByID(ID);
                }
                else if(cmd == "5")
                {
                    Console.WriteLine("Введите ID: ");
                    int ID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите имя: ");
                    string a = Console.ReadLine();
                    Console.WriteLine("Введите фамилию: ");
                    string b = Console.ReadLine();
                    Console.WriteLine("Введите отчество: ");
                    string c = Console.ReadLine();
                    Console.WriteLine("Введите дату рождения: ");
                    string d = Console.ReadLine();
                    sqlHelp.Update(ID, a, b, c, d);
                }
                Console.WriteLine(menu);
            }
        }
    }
}
