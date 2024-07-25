using NUnit.Framework;
using TowerOfHanoi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;

namespace TowerOfHanoi.Tests
{
    [TestFixture()]
    public class ProgramTests
    {
        //[Test()]
        //public void ToTakeDiscIfTowerIsFullTest()//localu kintmaji firstTower reiktu prilyginti 1
        //{
        //    //Arrange
        //    int tower = 1;
        //    int expected = 0;
        //    //Act            
        //    Program.FillTowersLists(5);
        //    int actual = Program.ToTakeDisc(tower);
        //    //Assert
        //    Assert.AreEqual(expected, actual);
        //}

        [Test()]
        public void ToPutDiskInEmptyTowerTest()
        {
            //Arrange
            int tower = 2;
            (int, int) expected = (0, 1);
            Program.FillTowersLists(5);
            //Act
            (int, int) actual = Program.ToPutDisk(tower);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void DiskcCreationDiskSize5Test()
        {
            //Arrange
            int discNumber = 5;
            string expected = "     XXXXX5XXXXX     ";
            //Act
            string actual = Program.DiskcCreation(discNumber);
            //Assert
            Assert.AreEqual(expected, actual);
        }        
    }
}