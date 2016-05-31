using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AddressBookProject.DTO;

namespace AddressBookProject
{
    public class AddressBook
    {
        private readonly List<User> _listOfUsers;

        public AddressBook()
        {
            _listOfUsers = new List<User>();
        }

        public event Action<User> UserAdded;
        public event Action<User> UserRemoved;

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.PhoneNumber != null)
            {
                if (user.PhoneNumber[0] != '+' && !('0' <= user.PhoneNumber[0] && user.PhoneNumber[0] <= '9'))
                {
                    throw new Exception("Invalid PhoneNumber");
                }
                for (var i = 1; i < user.PhoneNumber.Length; i++)
                {
                    if (!('0' <= user.PhoneNumber[i] && user.PhoneNumber[i] <= '9'))
                    {
                        throw new Exception("Invalid PhoneNumber");
                    }
                }
            }

            user.TimeAdded = DateTime.Now;
            _listOfUsers.Add(user);

            if (UserAdded != null)
            {
                UserAdded(user);
            }
        }

        public void RemoveUser(string id)
        {
            var user = _listOfUsers.FirstOrDefault(u => string.Equals(u.Id, id));
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            try
            {
                _listOfUsers.Remove(user);
            }
            catch (Exception)
            {
                throw new Exception("User not found");
            }

            if (UserRemoved != null)
            {
                UserRemoved(user);
            }
        }

        public IEnumerable<User> GetUsersEmailAdrress()
        {
            var users = _listOfUsers.Where(x => x.Email.Contains("gmail.com"));
            return users;
        }

        public IEnumerable GetUsersInKiev()
        {
            for (var i = 0; i < _listOfUsers.Count; i++)
            {
                var end = DateTime.Now;
                var start = _listOfUsers[i].Birthdate;
                var year = (int) (end - start).TotalDays/365;
                if (year > 18 && _listOfUsers[i].City == "Kiev") yield return _listOfUsers[i];
            }
        }

        public IEnumerable<User> GetGirlsAddedLastTenDays()
        {
            var users = from u in _listOfUsers
                let end = DateTime.Now
                let start = u.TimeAdded
                where (u.Gender == Gender.Female &&
                       end.DayOfYear + 365*(end.Year - 1) - start.DayOfYear - (365*start.Year - 1) <= 10)
                select u;
            return users;
        }

        public IEnumerable<User> GetUsersBornInJanuaryHaveAddressAndPhone()
        {
            var users =
                _listOfUsers.Where(u => u.Address != null && u.PhoneNumber != null && u.Birthdate.Month == 1)
                    .OrderByDescending(x => x.LastName);
            return users;
        }

        public IDictionary<Gender, List<User>> GetDictionaryOfMaleAndFemale()
        {
            var users = _listOfUsers.GroupBy(u => u.Gender).ToDictionary(u => u.Key, u => u.ToList());
            return users;
        }

        public IEnumerable<User> GetUsersFromCityBornToday(string city)
        {
            var users = from u in _listOfUsers
                where u.City == city && u.Birthdate.DayOfYear == DateTime.Now.DayOfYear
                select u;
            return users;
        }

        public IEnumerable<User> GetUserPaging()
        {
            var users = _listOfUsers.Where(u => u.Gender == Gender.Male).Skip(1).Take(1);
            return users;
        }

        public IEnumerable<User> GetUsersBornToday()
        {
            var users = _listOfUsers.Where(u => u.Birthdate.DayOfYear == DateTime.Now.DayOfYear);
            return users;
        }
    }
}
