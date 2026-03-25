using System;
using System.Collections.Generic;
using System.Linq;

namespace CoD_IntelligenceOps
{
    public enum OperatorStatus { Disponivel, EmMissao }

    public class Operator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Codename { get; set; }
        public string Specialty { get; set; }
        public OperatorStatus Status { get; set; } = OperatorStatus.Disponivel;

        public List<Mission> MissionHistory { get; set; } = new List<Mission>();

        public int Experience { get; set; } = 0;
        public int Level => (Experience / 100) + 1;
        public int MissionsCompleted => MissionHistory.Count(m => m.Status == MissionStatus.Concluida);

        public Operator(int id, string name, string codename, string specialty)
        {
            Id = id;
            Name = name;
            Codename = codename;
            Specialty = specialty;
        }

        public void GainExperience(Difficulty difficulty)
        {
            int xp = difficulty switch
            {
                Difficulty.Facil => 50,
                Difficulty.Media => 100,
                Difficulty.Dificil => 200,
                _ => 50
            };

            Experience += xp;
        }

        public override string ToString()
        {
            string status = Status == OperatorStatus.Disponivel ? "Disponível" : "Em missão";

            return $"ID: {Id}, Codename: {Codename}, Level: {Level}, XP: {Experience}, Specialty: {Specialty}, Status: {status}, Missões: {MissionHistory.Count}";
        }
    }
}