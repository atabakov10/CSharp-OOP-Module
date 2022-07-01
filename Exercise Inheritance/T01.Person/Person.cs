using System;
using System.Collections.Generic;
using System.Text;

namespace T01.Person
{
    public class Person
    {
        private string name;
        private int age;

        public virtual int Age
        {
            get { return age; }
            set {
               age = value;
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("Name: {0}, Age: {1}", this.Name, this.Age));
            return sb.ToString();
        }

    }
}
