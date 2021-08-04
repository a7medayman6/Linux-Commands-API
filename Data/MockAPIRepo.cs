using System.Collections.Generic;
using API.Models;

namespace API.Data
{
    public class MockAPIRepo : IAPIRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, Name="ls", Platform="Linux", Description="Lists all the files and dirs in the current directory."},
                new Command{Id=1, Name="pwd", Platform="Linux", Description="Prints the current working dir path."},
                new Command{Id=2, Name="ps", Platform="Docker", Description="Lists all the running containers."}
            };
            return commands;

        }

        public Command GetCommandById(int id)
        {
            return new Command{Id=0, Name="ls", Platform="Linux", Description="Lists all the files and dirs in the current directory."};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}