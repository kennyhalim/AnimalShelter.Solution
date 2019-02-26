using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalShelter.Models;
using System.Collections.Generic;
using System;

namespace AnimalShelter.Tests
{
  [TestClass]
  public class AnimalTest : IDisposable
  {

    public void Dispose()
    {
      Animal.ClearAll();
    }

    public AnimalTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=animal_shelter_test;";
    }

    [TestMethod]
    public void AnimalConstructor_CreatesInstanceOfAnimal_Animal()
    {
      Animal newAnimal = new Animal("test", "test", "test", Convert.ToDateTime("05/01/2019"));
      Assert.AreEqual(typeof(Animal), newAnimal.GetType());
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_AnimalList()
    {
      //Arrange
      List<Animal> newList = new List<Animal> { };

      //Act
      List<Animal> result = Animal.GetAll("");

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Animal()
    {
      // Arrange, Act
      Animal firstItem = new Animal("test", "test", "test", Convert.ToDateTime("05/01/2019"));
      Animal secondItem = new Animal("test", "test", "test", Convert.ToDateTime("05/01/2019"));

      // Assert
      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Animal testItem = new Animal("test", "test", "test", Convert.ToDateTime("05/01/2019"));

      //Act
      testItem.Save();
      List<Animal> result = Animal.GetAll("");
      List<Animal> testList = new List<Animal>{testItem};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      Animal newItem1 = new Animal("a", "test", "test", Convert.ToDateTime("05/01/2019"));
      newItem1.Save();
      Animal newItem2 = new Animal("b", "test", "test", Convert.ToDateTime("05/01/2019"));
      newItem2.Save();
      List<Animal> newList = new List<Animal> { newItem1, newItem2 };

      //Act
      List<Animal> result = Animal.GetAll("");

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsSortedItems_True()
    {
      //Arrange
      Animal newItem1 = new Animal("b", "test", "test", Convert.ToDateTime("05/01/2019"));
      newItem1.Save();
      Animal newItem2 = new Animal("a", "test", "test", Convert.ToDateTime("05/01/2019"));
      newItem2.Save();
      List<Animal> newList = new List<Animal> { newItem2, newItem1 };

      //Act
      List<Animal> result = Animal.GetAll("name");

      Assert.AreEqual(newList[0].GetName(), result[0].GetName());
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Animal testItem = new Animal("test", "test", "test", Convert.ToDateTime("05/01/2019"));

      //Act
      testItem.Save();
      Animal savedItem = Animal.GetAll("")[0];

      int result = savedItem.GetId();
      int testId = testItem.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
    {
      //Arrange
      Animal newItem = new Animal("test", "test", "test", Convert.ToDateTime("05/01/2019"));
      //Act
      int result = newItem.GetId();

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectItemFromDatabase_Item()
    {
      //Arrange
      Animal testItem = new Animal("test", "test", "test", Convert.ToDateTime("05/01/2019"));
      testItem.Save();

      //Act
      Animal foundItem = Animal.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }

  }
}
