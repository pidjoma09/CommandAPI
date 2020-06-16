using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTest:IDisposable
    {
        Command testCommand;

        public CommandTest()
        {
            var testCommand= new Command
            {
                HowTo="Do something awesome",
                Platform="xnit",
                CommandLine="dotnet test"
            };
        }
        public void Dispose()
        {
            testCommand=null;
        }


        [Fact]
        public void CanChangeHowTo()
        {

            //Arrange
            

            //Act 
            testCommand.HowTo="Execute Unit Tests";

            //Assert
            Assert.Equal("Execute Unit Tests",testCommand.HowTo);

        }

        public void CanChangePlateform()
        {
            
            //Arrange
            
            //Act
            testCommand.Platform="dotnet core";

            //Assert
            Equals("dotnet core",testCommand.Platform);
        }

        public void CanChangeCommandLine()
        {
            //Arrange
            

            //Act
            testCommand.CommandLine="dotnet core";

            //Assert
            Equals("dotnet core",testCommand.CommandLine);
        }

        
    }
    
}