using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CoD_IntelligenceOps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;

            while (true)
            {
                if (Login())
                    break;
                Console.Clear();
            }

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
                

                ShowHeader();

                Console.WriteLine("[01] Registrar Operador");
                Console.WriteLine("[02] Listar Operadores");
                Console.WriteLine("[03] Filtrar por Especialidade");

                Console.WriteLine("\n--- MISSÕES ---");
                Console.WriteLine("[04] Nova Missão");
                Console.WriteLine("[05] Listar Missões");
                Console.WriteLine("[06] Enviar Operador");
                Console.WriteLine("[07] Envio Aleatório");
                Console.WriteLine("[08] Alterar Status");

                Console.WriteLine("\n--- INTEL ---");
                Console.WriteLine("[9] Filtrar Missões");
                Console.WriteLine("[10] Gerar Dados");

                Console.WriteLine("\n--- CONTROLE ---");
                Console.WriteLine("[11] Excluir Operador");
                Console.WriteLine("[12] Excluir Missão");

                Console.WriteLine("\n--- COMANDO ---");
                Console.WriteLine("[13] Ranking");

                Console.WriteLine("\n--- CAMPANHA ---");
                Console.WriteLine("[14] Carregar Campanha");
                Console.WriteLine("[15] Jogar Campanha");

                Console.WriteLine("\n[99] Encerrar Sistema");

                Console.Write("\n>> ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterOperator();
                        break;
                    case "2":
                        ListOperators();
                        break;
                    case "3":
                        ListOperatorsBySpecialty();
                        break;
                    case "4":
                        RegisterMission();
                        break;
                    case "5":
                        ListMissions();
                        break;
                    case "6":
                        AssignOperator();
                        break;
                    case "7":
                        AssignRandomOperator();
                        break;
                    case "8":
                        ChangeMissionStatus();
                        break;
                    case "9":
                        FilterMissions();
                        break;
                    case "10":
                        GenerateTestData();
                        break;
                    case "11":
                        DeleteOperator();
                        break;
                    case "12":
                        DeleteMission();
                        break;
                    case "13": 
                        ShowRanking();
                        break;
                    case "14":
                        GenerateCampaign();
                        break;
                    case "15":
                        PlayCampaign();
                        break;
                    case "99":
                        running = false;
                        break;
                    default:
                        ShowMessage("Opção inválida!");
                        break;

                }
                Console.Clear();
            }
        }

        //metodo de login

        static bool Login()
        {
            Console.Clear();
            TypeEffect("INICIANDO TERMINAL SEGURO...");
            Thread.Sleep(800);

            TypeEffect("CONECTANDO AO SERVIDOR...");
            Thread.Sleep(800);

            Console.WriteLine("\n=== ACESSO RESTRITO ===\n");

            Console.Write("USER: ");
            string user = Console.ReadLine();

            Console.Write("PASS: ");
            string pass = Console.ReadLine();

            if (user == "olimpo" && pass == "olimpo")
            {
                TypeEffect("\n ACESSO AUTORIZADO");
                Thread.Sleep(1000);
                return true;
            }
            else
            {
                TypeEffect("\n DADOS CAPTURADOS...");
                TypeEffect(" RASTREANDO LOCALIZAÇÃO...");
                TypeEffect(" EQUIPE ENVIADA.");
                Thread.Sleep(2000);
                return false;
            }
        }
        //fim metodo login

        static void TypeEffect(string text, int delay = 25)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        static void ShowHeader()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("=========================================");
            Console.WriteLine("   CoD INTELLIGENCE OPS - TERMINAL");
            Console.WriteLine("=========================================\n");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Status: ONLINE | {DateTime.Now}");
            Console.WriteLine("Acesso: CLASSIFICADO");
            Console.WriteLine("-----------------------------------------\n");

            Console.ForegroundColor = ConsoleColor.Green;
        }



        // Utilitário para mostrar mensagens com pausa
        static void ShowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            TypeEffect("\n[SYS] " + message);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPressione qualquer tecla...");
            Console.ReadKey();
        }

        // Registro de operadores
        static void RegisterOperator()
        {
            Console.Write("Nome real: ");
            var name = Console.ReadLine();
            Console.Write("Codinome: ");
            var codename = Console.ReadLine();
            Console.Write("Especialidade: ");
            var specialty = Console.ReadLine();

            Database.AddOperator(name, codename, specialty);
            ShowMessage("Operador cadastrado com sucesso!");
        }

        // Listar operadores
        static void ListOperators()
        {
            var operators = Database.GetOperators();
            if (operators.Count == 0)
            {
                ShowMessage("Nenhum operador cadastrado.");
                return;
            }

            Console.WriteLine("\n=== Lista de Operadores ===");
            foreach (var op in operators)
            {
                Console.WriteLine(op);
            }
            ShowMessage("");
        }

        // Listar operadores por especialidade
        static void ListOperatorsBySpecialty()
        {
            Console.Write("Digite a especialidade: ");
            var specialty = Console.ReadLine();

            var filtered = Database.GetOperators()
                                   .Where(o => o.Specialty.Equals(specialty, StringComparison.OrdinalIgnoreCase))
                                   .ToList();

            if (filtered.Count == 0)
            {
                ShowMessage("Nenhum operador encontrado com essa especialidade.");
                return;
            }

            Console.WriteLine($"\n=== Operadores com especialidade {specialty} ===");
            foreach (var op in filtered)
            {
                Console.WriteLine(op);
            }
            ShowMessage("");
        }

        // Registro de missão
        static void RegisterMission()
        {
            Console.Write("Nome da missão: ");
            var name = Console.ReadLine();
            Console.Write("Objetivo: ");
            var objective = Console.ReadLine();

            Console.WriteLine("Escolha a dificuldade:\n(1 - Fácil, 2 - Média, 3 - Difícil) ");
            var diffInput = Console.ReadLine();
            Difficulty diff = Difficulty.Facil;
            Enum.TryParse(diffInput, true, out diff);

            Database.AddMission(name, objective, diff);
            ShowMessage("Missão cadastrada com sucesso!");
        }

        // Listar missões
        static void ListMissions()
        {
            var missions = Database.GetMissions();
            if (missions.Count == 0)
            {
                ShowMessage("Nenhuma missão cadastrada.");
                return;
            }

            Console.WriteLine("\n=== Lista de Missões ===");
            foreach (var m in missions)
            {
                Console.WriteLine(m);
            }
            ShowMessage("");
        }

        // Enviar operador para missão
        static void AssignOperator()
        {
            ListOperators();
            Console.Write("Digite o ID do operador: ");
            if (!int.TryParse(Console.ReadLine(), out int opId))
            {
                ShowMessage("ID inválido!");
                return;
            }

            var op = Database.GetOperators().FirstOrDefault(o => o.Id == opId);
            if (op.Status == OperatorStatus.EmMissao)
            {
                ShowMessage("Operador está ocupado em outra missão!");
                return;
            }

            ListMissions();
            Console.Write("Digite o ID da missão: ");
            if (!int.TryParse(Console.ReadLine(), out int missionId))
            {
                ShowMessage("ID inválido!");
                return;
            }

            var mission = Database.GetMissions().FirstOrDefault(m => m.Id == missionId);

            Database.AssignOperatorToMission(opId, missionId);
            op.MissionHistory.Add(mission);
            op.Status = OperatorStatus.EmMissao;

            ShowRandomMessage(op.Codename, mission.Name);
        }

        // Enviar operador aleatório para missão
        static void AssignRandomOperator()
        {
            var availableOperators = Database.GetOperators().Where(o => o.Status == OperatorStatus.Disponivel).ToList();
            var missions = Database.GetMissions();

            if (availableOperators.Count == 0 || missions.Count == 0)
            {
                ShowMessage("Não há operadores disponíveis ou missões cadastradas.");
                return;
            }

            var rand = new Random();
            var op = availableOperators[rand.Next(availableOperators.Count)];
            var mission = missions[rand.Next(missions.Count)];

            Database.AssignOperatorToMission(op.Id, mission.Id);
            op.MissionHistory.Add(mission);
            op.Status = OperatorStatus.EmMissao;

            ShowRandomMessage(op.Codename, mission.Name);
        }

        // Mostrar mensagem aleatória
        static void ShowRandomMessage(string codename, string mission)
        {
            string[] messages = {
                $"Operador {codename} infiltrado com sucesso em {mission}!",
                $"Missão {mission} concluída com sucesso por {codename}!",
                $"{codename} alerta: inimigos detectados na missão {mission}!",
                $"Operador {codename} completou a infiltração em {mission} sem ser detectado!"
            };

            var rand = new Random();
            ShowMessage(messages[rand.Next(messages.Length)]);
        }

        // Alterar status da missao
        static void ChangeMissionStatus()
        {
            ListMissions();
            Console.Write("ID da missão: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            var mission = Database.GetMissions().FirstOrDefault(m => m.Id == id);
            if (mission == null)
            {
                ShowMessage("Missão não encontrada.");
                return;
            }

            Console.WriteLine("1-Em andamento | 2-Falha | 3-Concluída");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out int status))
                return;

            mission.Status = (MissionStatus)status;

            if (mission.Status == MissionStatus.Concluida)
            {
                foreach (var op in mission.AssignedOperators)
                {
                    op.GainExperience(mission.Difficulty);
                    op.Status = OperatorStatus.Disponivel;
                }
            }

            ShowMessage("Status atualizado!");
        }

        // Filtrar missões
        static void FilterMissions()
        {
            Console.WriteLine("Filtrar por: 1 - Em andamento, 2 - Concluídas, 3 - Por operador");
            var input = Console.ReadLine();

            List<Mission> filtered = new List<Mission>();
            if (input == "1")
            {
                filtered = Database.GetMissions().Where(m => m.Status == MissionStatus.EmAndamento).ToList();
            }
            else if (input == "2")
            {
                filtered = Database.GetMissions().Where(m => m.Status == MissionStatus.Concluida).ToList();
            }
            else if (input == "3")
            {
                ListOperators();
                Console.Write("Digite o ID do operador: ");
                if (int.TryParse(Console.ReadLine(), out int opId))
                {
                    filtered = Database.GetMissions().Where(m => m.AssignedOperators.Any(o => o.Id == opId)).ToList();
                }
            }

            if (filtered.Count == 0)
            {
                ShowMessage("Nenhuma missão encontrada com os critérios selecionados.");
                return;
            }

            Console.WriteLine("\n=== Missões filtradas ===");
            foreach (var m in filtered)
            {
                Console.WriteLine(m);
            }
            ShowMessage("");
        }
        static void DeleteOperator()
        {
            ListOperators();
            Console.Write("ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            Console.Write("Confirmar (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s")
                return;

            Database.RemoveOperator(id, out string msg);
            ShowMessage(msg);
        }

        static void DeleteMission()
        {
            ListMissions();
            Console.Write("ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            Console.Write("Confirmar (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s")
                return;

            Database.RemoveMission(id, out string msg);
            ShowMessage(msg);
        }

        static void ShowRanking()
        {
            var ranking = Database.GetOperators()
                .OrderByDescending(o => o.MissionsCompleted)
                .ThenByDescending(o => o.Level)
                .ToList();

            Console.WriteLine("\n=== RANKING ===");

            int pos = 1;
            foreach (var op in ranking)
            {
                Console.WriteLine($"{pos}º {op.Codename} | Missões: {op.MissionsCompleted} | Level: {op.Level}");
                pos++;
            }

            ShowMessage("");
        }
        static int GetNextCampaignMissionOrder()
        {
            var completedOrders = Database.GetMissions()
                .Where(m => m.IsCampaign && m.Status == MissionStatus.Concluida)
                .Select(m => m.CampaignOrder);

            int next = 1;

            while (completedOrders.Contains(next))
                next++;

            return next;
        }

        static void PlayCampaign()
        {
            // Filtrar missões de campanha
            var campaignMissions = Database.GetMissions().Where(m => m.IsCampaign).ToList();
            if (campaignMissions.Count == 0)
            {
                ShowMessage("Nenhuma campanha carregada. Carregue a campanha antes de jogar!");
                return;
            }

            // Determinar próxima missão não concluída
            int nextOrder = GetNextCampaignMissionOrder();
            var mission = campaignMissions.FirstOrDefault(m => m.CampaignOrder == nextOrder);

            if (mission == null)
            {
                ShowMessage("Campanha concluída! Todas as missões de campanha foram finalizadas.");
                return;
            }

            // Checar operadores disponíveis
            var availableOperators = Database.GetOperators()
                .Where(o => o.Status == OperatorStatus.Disponivel)
                .ToList();

            if (availableOperators.Count == 0)
            {
                ShowMessage("Nenhum operador disponível para a missão da campanha!");
                return;
            }

            // Mostrar detalhes da missão
            Console.Clear();
            Console.WriteLine($"=== MISSÃO {mission.CampaignOrder} ===");
            Console.WriteLine($"Nome: {mission.Name}");
            Console.WriteLine($"Objetivo: {mission.Objective}");
            Console.WriteLine($"Dificuldade: {mission.Difficulty}\n");

            // Listar operadores disponíveis

            ShowMissionStory(mission);
            Console.WriteLine("Escolha um operador disponível:");
            foreach (var op in availableOperators)
            {
                Console.WriteLine($"ID: {op.Id}, Codename: {op.Codename}, Level: {op.Level}, Specialty: {op.Specialty}");
            }

            if (!int.TryParse(Console.ReadLine(), out int opId))
            {
                ShowMessage("Entrada inválida!");
                return;
            }

            var selectedOp = availableOperators.FirstOrDefault(o => o.Id == opId);
            if (selectedOp == null)
            {
                ShowMessage("Operador inválido ou ocupado!");
                return;
            }

            // Iniciar missão
            Console.WriteLine("\nIniciando missão...");
            Thread.Sleep(1500);
            Console.WriteLine("Confronto em andamento...");
            Thread.Sleep(1500);
            Console.WriteLine("Objetivo cumprido!");
            Thread.Sleep(1500);

            // Concluir missão
            mission.Status = MissionStatus.Concluida;
            selectedOp.GainExperience(mission.Difficulty);
            selectedOp.Status = OperatorStatus.Disponivel;
            selectedOp.MissionHistory.Add(mission);

            ShowMessage($"Missão concluída! {selectedOp.Codename} ganhou XP!");
            Console.Clear();
        }

        static void ShowMissionMap()
        {
            Console.WriteLine(@"
            ┌───────────────────────────────┐
            │         BASE INIMIGA          │
            │                               │
            │  ██████      ██████           │
            │  █    █      █    █           │
            │  █    ████████    █           │
            │  █                █           │
            │  ██████████████████           │
            │                               │
            │   START →           TARGET    │
            └───────────────────────────────┘
                                ");
            Console.WriteLine("Legenda:");
            Console.WriteLine("█ = Estrutura inimiga");
            Console.WriteLine("START = Ponto de infiltração");
            Console.WriteLine("TARGET = Objetivo da missão");
        }

        static void ShowMissionStory(Mission mission)
        {
            Console.Clear();
            Console.WriteLine("=== RELATÓRIO DE MISSÃO ===\n");
            Console.WriteLine($"Nome da missão: {mission.Name}");
            Console.WriteLine($"Objetivo: {mission.Objective}\n");

            Console.WriteLine("Narrativa:");
            Console.WriteLine("Inteligência confirmou a presença de forças inimigas fortemente armadas. " +
                              "O operador selecionado deve infiltrar a base, neutralizar os guardas e completar o objetivo sem ser detectado. " +
                              "Cada passo é crítico; uma decisão errada pode comprometer toda a operação.\n");

            ShowMissionMap();

            Console.WriteLine("\nPrepare-se! Pressione qualquer tecla para prosseguir...");
            Console.ReadKey();
        }



        static void GenerateTestData()
        {
            // Operadores de teste
            Database.AddOperator("Ethan Harper", "Hades", "Night OPS");
            Database.AddOperator("Matthew Parker", "Poseidon", "Navy OPS");
            Database.AddOperator("Renato Alves", "Ares", "Demolition OPS");
            Database.AddOperator("Ivy Armstrong", "Artemis", "Disguise OPS");
            Database.AddOperator("Pietro Hernandez", "Hermes", "Speed OPS");

            // Missões de teste
            Database.AddMission("Operação Bravo", "Neutralizar base inimiga", Difficulty.Media);
            Database.AddMission("Operação Alpha", "Recuperar intel secreto", Difficulty.Facil);
            Database.AddMission("Operação Charlie", "Destruir suprimentos inimigos", Difficulty.Dificil);
            Database.AddMission("Operação Delta", "Evacuação de VIP", Difficulty.Media);
            Database.AddMission("Operação Echo", "Proteção de civis", Difficulty.Facil);

            ShowMessage("Dados de teste gerados: 5 operadores e 5 missões adicionados!");
        }

        static void GenerateCampaign()
        {
            Database.AddCampaignMission("Infiltração Inicial", "Invadir base inimiga", Difficulty.Facil, 1);
            Database.AddCampaignMission("Sabotagem", "Destruir comunicações", Difficulty.Media, 2);
            Database.AddCampaignMission("Caça ao Líder", "Eliminar alvo prioritário", Difficulty.Dificil, 3);
            Database.AddCampaignMission("Extração", "Evacuar com segurança", Difficulty.Media, 4);

            ShowMessage("Campanha carregada!");
        }
    }
}