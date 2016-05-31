using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AddressBookProject;
using AddressBookProject.DTO;
using LoggerProject;
using System.Timers;

namespace MainProject
{
    public class Program
    {
        public static void Main()
        {
            

            var dir = Directory.GetCurrentDirectory();
            var match = Regex.Match(dir, "(\\\\bin\\\\(Debug|Release))");
            if (match.Success)
            {
                dir = dir.Replace(match.Value, String.Empty);
            }
            var loggerFactory = new LoggerFactory(dir + "\\Log\\Log.txt");

            ILogger logger;
            AddressBook addressBook;
            Program program;

            

            var user = new User
            {
                FirstName = "Andriy",
                LastName = "Dudka",
                Address = "Bestuzina 12/7",
                Birthdate = new DateTime(1994, 5, 31),
                PhoneNumber = "+380668839420",
                Email = "andriy.dudka@gmail.com",
                City = "Kiev",
                Gender = Gender.Male
            };      
          
            #region Console Optput test

            /*logger = loggerFactory.CreateLogger(0);
            addressBook = new AddressBook();
            program = new Program(logger);

            addressBook.UserAdded += program.OnUserAddedHandler;
            addressBook.UserRemoved += program.OnUserRemovedHandler;

            try
            {
                addressBook.AddUser(user);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
            
            try
            {
                addressBook.RemoveUser(user.Id);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }*/

            #endregion

            #region File Optput test

           /* logger = loggerFactory.CreateLogger(1);
            addressBook = new AddressBook();
            program = new Program(logger);

            addressBook.UserAdded += program.OnUserAddedHandler;
            addressBook.UserRemoved += program.OnUserRemovedHandler;

            try
            {
                addressBook.AddUser(user);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            try
            {
                addressBook.RemoveUser(user.Id);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }*/
                      
            #endregion       

            TestingLINQ();

            #region Notifier

            bool flag = false;
            addressBook = new AddressBook();
            addressBook.AddUser(user);
            var timer = new Timer();         
            timer.Interval = 1000;
            timer.Elapsed += (sender, e) =>
            {
                if (DateTime.Now.Hour == 12 && !flag)
                {
                    flag = true;
                    var users = addressBook.GetUsersBornToday();                
                    Console.WriteLine("Send mail all people who was born today :)");
                }
                if (DateTime.Now.Hour != 12)
                {
                    flag = false;
                }
            };
            timer.Start();

            #endregion
           
            Console.ReadKey();
        }

        private readonly ILogger _logger;       

        Program(ILogger logger)
        {
            _logger = logger;
            
        }

        void OnUserAddedHandler(User user)
        {
            _logger.Info(
                    string.Format("Successfully added user: {0}", user.Id));
        }

        void OnUserRemovedHandler(User user)
        {
            if (user == null)
            {
                _logger.Warning("User not found");
                return;
            }

            _logger.Info(
                string.Format("Successfully removed user: {0}", user.Id));
        }

        public static void TestingLINQ()
        {
            AddressBook addressBook = new AddressBook();

            var user = new User
            {
                FirstName = "Andriy",
                LastName = "Dudka",
                Address = "Bestuzina 12/7",
                Birthdate = new DateTime(1994, 5, 30),
                PhoneNumber = "+380668839420",
                Email = "andriy.dudka@gmail.com",
                City = "Kiev",
                Gender = Gender.Male
            }; 
            addressBook.AddUser(user);

            user = new User
            {
                FirstName = "Bob",
                LastName = "Marley",
                Address = "Bestuzina 12/7",
                Birthdate = new DateTime(1994, 1, 5),
                PhoneNumber = "+380668839420",
                Email = "bob.marley@gmail.com",
                City = "Kiev",
                Gender = Gender.Male
            };
            addressBook.AddUser(user);

            user = new User
            {
                FirstName = "Baba",
                LastName = "Jaga",
                Address = "Kurinnna_Noga",
                Birthdate = new DateTime(1880, 1, 23),
                PhoneNumber = "+380668839420",
                Email = "baba.jozhka@mail.ru",
                City = "Uzhgorod",
                Gender = Gender.Female
            };
            addressBook.AddUser(user);


            //юзеры у которых Email-адрес имеет домен “gmail.com”;
            /*var users = addressBook.GetUsersEmailAdrress();
            foreach (var it in users)
            {
                Console.WriteLine("LastName: {0}\nFirstName: {1}\nEmail: {2}\n", it.LastName, it.FirstName, it.Email);
            }*/

            //юзеры больше 18-ти лет и которые проживают в Киеве
            /*foreach (User it in addressBook.GetUsersInKiev())
            {
                Console.WriteLine("LastName: {0}\nFirstName: {1}\nCity: {2}\n", it.LastName, it.FirstName, it.City);
            }*/

            //юзеры, которые являются девушками и были добавлены за последние 10 дней;
            /*var users = addressBook.GetGirlsAddedLastTenDays();
            foreach (var it in users)
            {
                Console.WriteLine("LastName: {0}\nFirstName: {1}\nTimeAdded: {2}\n", 
                    it.LastName, it.FirstName, it.TimeAdded);
            }*/

            //юзеры которые родились в январе, и при этом имеют заполненые поля адреса и - телефона.
            /*var users = addressBook.GetUsersBornInJanuaryHaveAddressAndPhone();
            foreach (var it in users)
            {
                Console.WriteLine("LastName: {0}\nFirstName: {1}\nBirthDate: {2}\nAddress: {3}\nPhoneNumber: {4}\n", 
                    it.LastName, it.FirstName, it.Birthdate, it.Address, it.PhoneNumber);
            }*/

            //словарь, имеющий два ключа “man” и “woman”. По каждому из ключей словарь должен содержать список пользователей, 
            //которые соответствуют ключу словаря
            /*var users = addressBook.GetDictionaryOfMaleAndFemale();
            foreach (var it in users)
            {
                Console.WriteLine(it.Key);
                foreach (var u in it.Value)
                {
                    Console.Write(u.FirstName + " ");
                }
                Console.WriteLine("\n");
            }*/

            //юзеров, передавая произвольное условие (лямбда - выражение) 
            //и два параметра - с какого элемента выбирать и по какой (paging).
            /*var users = addressBook.GetUserPaging();
            foreach (var it in users)
            {
                Console.WriteLine("LastName: {0}\nFirstName: {1}\nCity: {2}\n", it.LastName, it.FirstName, it.City);
            }*/

            //количество пользователей, из города (передать в параметрах), у которых сегодня день рождения.
            /*var users = addressBook.GetUsersFromCityBornToday("Kiev");
            foreach (var it in users)
            {
                Console.WriteLine("LastName: {0}\nFirstName: {1}\nCity: {2}\n", it.LastName, it.FirstName, it.City);
            }*/


        }         
    }
}