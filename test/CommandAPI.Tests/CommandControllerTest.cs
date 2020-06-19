using System;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CommandAPI.Controllers;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandControllerTests:IDisposable
    {
        DbContextOptionsBuilder<CommandContext> optionsBuilder;
        CommandContext dbContext;
        CommandsController controller;
        public CommandControllerTests()
        {
           optionsBuilder=new DbContextOptionsBuilder<CommandContext>();
           optionsBuilder.UseInMemoryDatabase("UnitTestInMemBD");
           dbContext=new CommandContext(optionsBuilder.Options);

           controller=new CommandsController(dbContext);
        }

        public void Dispose()
        {
            optionsBuilder=null;
            foreach (var cmd in dbContext.CommandItems)
            {
                dbContext.CommandItems.Remove(cmd);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            controller=null;

        }

        //Action 1 Tests: get      /api/commands

        //TEST 1.1 REQUEST OBJECTS WHEN NONE EXIST   -RETURN "NOTHING"
        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //ARRANGE
            //DBContext

            
            //controller
            //var controller=new CommandsController(dbContext);

            //ACT
            var result=controller.GetCommandItems();

            //ASSERT
            Assert.Empty(result.Value);
        }     


        [Fact]
        public void GetCommandItemsReturnsOneItemWhenDBHasOneObject()
        {
            //Arrange 
            var command = new Command
            {
                HowTo="do Something",
                Platform="Some Platform",
                CommandLine="Some Command"
            };

            dbContext.CommandItems.Add(command);
            dbContext.SaveChanges();

            //ACT
            var result=controller.GetCommandItems();

            //Assert

            Assert.Single(result.Value);
        }


        [Fact]
        public void GetCommandItemsReturnsOneItemWhenDBHasNObject()
        {
            //ARRANGE
            var command= new Command()
            {
                
                HowTo="do Something",
                Platform="Some Platform",
                CommandLine="Some Command"
            
            };

            var command2= new Command()
            {
                
                HowTo="do Something",
                Platform="Some Platform",
                CommandLine="Some Command"
            
            };
            dbContext.Add(command);
            dbContext.Add(command2);
            dbContext.SaveChanges();

            //ACT
            var result=controller.GetCommandItems();

            //Assert
            Assert.Equal(2,result.Value.Count());
        }
        //Test 4
        [Fact]
        public void GetCommandItemsReturnsTheCorrectType()
        {
            //Arrange

            //ACt
            var result =controller.GetCommandItems();

            //Asert
            Assert.IsType<ActionResult<IEnumerable<Command>>>(result);
        }


    }
}