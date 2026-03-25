using System;
using System.Collections.Generic;
using System.Linq;

namespace CoD_IntelligenceOps
{
    public class Squad
    {
        public string Name { get; set; }
        public List<Operator> Operators { get; set; } = new List<Operator>();

        public Squad(string name)
        {
            Name = name;
        }
    }

    public static class Database
    {
        public static List<Operator> operators = new List<Operator>();
        public static List<Mission> missions = new List<Mission>();
        public static List<Squad> squads = new List<Squad>();

        private static int nextOperatorId = 1;
        private static int nextMissionId = 1;

        public static void AddOperator(string name, string codename, string specialty)
        {
            var op = new Operator(nextOperatorId++, name, codename, specialty);
            operators.Add(op);
        }

        public static List<Operator> GetOperators() => operators;

        public static void AddMission(string name, string objective, Difficulty diff)
        {
            var mission = new Mission(nextMissionId++, name, objective, diff);
            missions.Add(mission);
        }

        public static List<Mission> GetMissions() => missions;

        public static bool AssignOperatorToMission(int operatorId, int missionId)
        {
            var op = operators.FirstOrDefault(o => o.Id == operatorId);
            var mission = missions.FirstOrDefault(m => m.Id == missionId);

            if (op == null || mission == null) return false;

            mission.AssignedOperators.Add(op);
            return true;
        }

        // exculir operador
        public static bool RemoveOperator(int id, out string message)
        {
            var op = operators.FirstOrDefault(o => o.Id == id);

            if (op == null)
            {
                message = "Operador não encontrado.";
                return false;
            }

            if (op.Status == OperatorStatus.EmMissao)
            {
                message = "Operador está em missão!";
                return false;
            }

            foreach (var squad in squads)
                squad.Operators.Remove(op);

            foreach (var mission in missions)
                mission.AssignedOperators.Remove(op);

            operators.Remove(op);

            message = $"Operador {op.Codename} removido!";
            return true;
        }

        // excluir missao
        public static bool RemoveMission(int id, out string message)
        {
            var mission = missions.FirstOrDefault(m => m.Id == id);

            if (mission == null)
            {
                message = "Missão não encontrada.";
                return false;
            }

            foreach (var op in mission.AssignedOperators)
                op.Status = OperatorStatus.Disponivel;

            missions.Remove(mission);

            message = $"Missão {mission.Name} removida!";
            return true;
        }

        // Squads
        public static void CreateSquad(string name)
        {
            squads.Add(new Squad(name));
        }

        public static Squad GetSquad(string name) =>
            squads.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public static void ListSquads()
        {
            if (squads.Count == 0)
            {
                Console.WriteLine("Nenhum squad cadastrado.");
                return;
            }

            Console.WriteLine("\n=== Squads ===");
            foreach (var s in squads)
            {
                string ops = s.Operators.Count > 0
                    ? string.Join(", ", s.Operators.Select(o => o.Codename))
                    : "Nenhum operador";

                Console.WriteLine($"Squad: {s.Name}, Operadores: {ops}");
            }
        }
        public static void AddCampaignMission(string name, string objective, Difficulty diff, int order)
        {
            var mission = new Mission(nextMissionId++, name, objective, diff, order, true);
            missions.Add(mission);
        }
    }
}