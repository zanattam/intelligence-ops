using System;
using System.Collections.Generic;
using System.Linq;

namespace CoD_IntelligenceOps
{
    public enum MissionStatus 
    {
        EmAndamento = 1,
        Falha = 2,
        Concluida = 3 
    }

    public enum Difficulty 
    {
        Facil = 1,
        Media = 2,
        Dificil = 3 
    }

    public class Mission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Objective { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<Operator> AssignedOperators { get; set; } = new List<Operator>();
        public MissionStatus Status { get; set; } = MissionStatus.EmAndamento;
        public int CampaignOrder { get; set; }
        public bool IsCampaign { get; set; } = false;

        public Mission(int id, string name, string objective, Difficulty diff, int campaignOrder = 0, bool isCampaign = false)
        {
            Id = id;
            Name = name;
            Objective = objective;
            Difficulty = diff;
            CampaignOrder = campaignOrder;
            IsCampaign = isCampaign;
        }

        public void ConcludeMission() => Status = MissionStatus.Concluida;

        public override string ToString()
        {
            string ops = AssignedOperators.Count > 0
                ? string.Join(", ", AssignedOperators.Select(o => o.Codename))
                : "Nenhum operador atribuído";

            return $"ID: {Id}, Missão: {Name}, Objetivo: {Objective}, Dificuldade: {Difficulty}, Operadores: {ops}, Status: {Status}";
        }
    }
}