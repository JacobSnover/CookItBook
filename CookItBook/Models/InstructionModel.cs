using CookItBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItBook.Models
{
    public class InstructionModel
    {
        public int ID { get; set; }
        public string Step { get; set; }

        public InstructionModel() { }

        public InstructionModel(string step)
        {
            Step = step;
        }

        public InstructionModel(string step, int id)
        {
            Step = step;
            ID = id;
        }

        #region mappers

        public static List<InstructionModel> DtoToModelToList(ICollection<Instruction> instructions)
        {
            List<InstructionModel> instructionList = new List<InstructionModel>();

            foreach (Instruction instruction in instructions)
            {
                instructionList.Add(new InstructionModel(instruction.Value, instruction.Key));
            }

            return instructionList;
        }

        public static ICollection<Instruction> ModelToDto(IEnumerable<InstructionModel> instructions)
        {
            List<Instruction> instructionList = new List<Instruction>();
            foreach (var instruction in instructions)
            {
                instructionList.Add(ModelToDto(instruction));
            }
            return instructionList;
        }

        public static Instruction ModelToDto(InstructionModel instruction)
        {
            return new Instruction()
            {
                Key = instruction.ID,
                Value = instruction.Step.Trim()
            };
        }

        public static ICollection<InstructionModel> instructionCSVtoDTO(string instructionCSV)
        {
            List<InstructionModel> instructionList = new List<InstructionModel>();

            try
            {
                string[] instructions = instructionCSV.Split('|');
                foreach (string instruction in instructions)
                {
                    if (instruction != "")
                        instructionList.Add(new InstructionModel(instruction.Trim()));
                }
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine("instruction" + e.Message);
            }

            return instructionList;
        }


        #endregion
    }
}
