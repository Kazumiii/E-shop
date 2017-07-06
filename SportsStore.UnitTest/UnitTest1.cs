﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using System.Linq;
 
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProduktRepository> mock = new Mock<IProduktRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductID=1,Name="P1" },
                    new Product {ProductID=2,Name="P2" },
                    new Product {ProductID=3,Name="P3" },
                    new Product {ProductID=4,Name="P4" },
                    new Product {ProductID=5,Name="P5" },
                }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            ProductListViewModel results=(ProductListViewModel) controller.List(2).Model;
            Product[] prodArray=results.Products.ToArray();
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

         [TestMethod]
         public void Can_Send_Pagination_View_Model()
        {
            Mock<IProduktRepository> mock = new Mock<IProduktRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]

                {
                    new Product { ProductID=1,Name="P1"},
                    new Product {ProductID=2,Name="P2" },
                    new Product {ProductID=3,Name="P3" },
                    new Product { ProductID=4,Name="P4" },
                    new Product {ProductID=5 ,Name="P5"},
                }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            ProductListViewModel results=(ProductListViewModel)controller.List(2).Model;

            PagingInfo info = results.PagingINfo;
            Assert.AreEqual(info.CurrentPage, 2);
            Assert.AreEqual(info.ItemsPerPage, 3);
            Assert.AreEqual(info.TotalItems, 5);
            Assert.AreEqual(info.TotalPages, 2);
        }
    }
}