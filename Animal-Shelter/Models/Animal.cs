using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;


namespace AnimalShelter.Models
{
  public class Animal
  {
    string _name;
    string _sex;
    string _breed;
    DateTime _dateOfAdmittance = new DateTime();
    int _id;
    public Animal (string name, string sex, string breed, DateTime dateOfAdmittance, int id = 0)
    {
      _name = name;
      _sex = sex;
      _breed = breed;
      _dateOfAdmittance = dateOfAdmittance;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetSex()
    {
      return _sex;
    }

    public string GetBreed()
    {
      return _breed;
    }

    public DateTime GetDateOfAdmittance()
    {
      return _dateOfAdmittance;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Animal> GetAll(string sort)
    {
      List<Animal> allAnimals = new List<Animal> {};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      if (string.IsNullOrEmpty(sort))
        cmd.CommandText = @"SELECT * FROM animal;";
      else
      {
        cmd.CommandText = @"SELECT * FROM animal ORDER BY " + sort + ";";
        // MySqlParameter paramSort = new MySqlParameter();
        // paramSort.ParameterName = "@sort";
        // paramSort.Value = sort;
        // cmd.Parameters.Add(paramSort);
      }

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(4);
        string animalName = rdr.GetString(0);
        string animalSex = rdr.GetString(1);
        string animalBreed = rdr.GetString(2);
        DateTime animalDateOfAdmittance = rdr.GetDateTime(3);
        Animal newAnimal = new Animal(animalName,animalSex,animalBreed,animalDateOfAdmittance,animalId);
        allAnimals.Add(newAnimal);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allAnimals;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM animal;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO animal (name, sex, breed, dateofadmittance) VALUES (@AnimalName, @AnimalSex, @AnimalBreed, @AnimalDateOfAdmittance);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@AnimalName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter sex = new MySqlParameter();
      sex.ParameterName = "@AnimalSex";
      sex.Value = this._sex;
      cmd.Parameters.Add(sex);

      MySqlParameter breed = new MySqlParameter();
      breed.ParameterName = "@AnimalBreed";
      breed.Value = this._breed;
      cmd.Parameters.Add(breed);

      MySqlParameter dateofadmittance = new MySqlParameter();
      dateofadmittance.ParameterName = "@AnimalDateOfAdmittance";
      dateofadmittance.Value = this._dateOfAdmittance;
      cmd.Parameters.Add(dateofadmittance);

      cmd.ExecuteNonQuery();    // This line is new!
      _id = (int)cmd.LastInsertedId;
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
    }

    public static Animal Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM animal WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int animalId = 0;
      string animalName = "";
      string animalSex = "";
      string animalBreed = "";
      DateTime animalDateOfAdmittance = new DateTime();
      while(rdr.Read())
      {
        animalId = rdr.GetInt32(4);
        animalName = rdr.GetString(0);
        animalSex = rdr.GetString(1);
        animalBreed = rdr.GetString(2);
        animalDateOfAdmittance = rdr.GetDateTime(3);
      }
      Animal newAnimal = new Animal(animalName, animalSex, animalBreed, animalDateOfAdmittance, animalId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newAnimal;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Animal))
      {
        return false;
      }
      else
      {
        Animal newItem = (Animal) otherItem;
        return (this.GetName() == newItem.GetName() && this.GetId() == newItem.GetId() && this.GetBreed() == newItem.GetBreed() && this.GetSex() == newItem.GetSex() && this.GetDateOfAdmittance() == newItem.GetDateOfAdmittance());
      }
    }
  }
}
