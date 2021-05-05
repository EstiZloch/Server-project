using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyTravelDAL;
namespace SimpltyTravelBLL.dijkstra
{
    class Dijkstra
    {
//        // Our dictionary of nodes. Allows us to quickly change a nodes value
//        // through its name (the key).
//        static Dictionary<int, Node> nodeDict = new Dictionary<int, Node>();
//        // The list of routes.
//        static List<Route> routes = new List<Route>();

//        // This set allows us to quickly check which nodes we have already
//        // visited.
//        static HashSet<int> unvisited = new HashSet<int>();

//        //   const string graphFilePath;

//        public void DijkstraExecute(List<int> listOfSitesKindsCode, Criterion c)
//        {
//            //initializate the return list to the address of the client
//            List<Sites> returnList = new List<Sites>();
//            //a filter function
//            //SiteBL s = new SiteBL();
//            //listOfGoodSites=s.Filter(c);

//            for (int i = 0; i < listOfSitesKindsCode.Count; i++)
//            {
//                try
//                {
//                    initGraph(listOfSitesKindsCode[i], returnList);
//                }
//                catch (FileNotFoundException ex)
//                {
//                    Console.WriteLine(ex.Message);
//                    return;
//                }
//                Console.Clear();
//                PrintOverview();
//                int startNode = returnList.Last().CodeSite;
//                // Set our start node. The start node has to have a value
//                // of 0 because we're already there.
//                nodeDict[startNode].Value = 0;
//                ////var queue = new PrioQueue();
//                ////queue.AddNodeWithPriority(nodeDict[startNode]);

//                //// Do the calculations to find the shortest path to every node
//                //// in the graph starting from our starting node.
//                //// If there are no nodes left to check in our queue, we're done.
//                ////if (queue.Count == 0)
//                ////{
//                ////    return;
//                ////}

//                //foreach (var route in routes.FindAll(r => r.From == queue.First.Value.Name))
//                //{
//                //    // Skip routes to nodes that have already been visited.
//                //    if (!unvisited.Contains(route.To))
//                //    {
//                //        continue;
//                //    }

//                //    double travelledDistance = nodeDict[queue.First.Value.Name].Value + route.Distance;

//                //    // We only look at nodes we haven't visited yet and we only
//                //    // update the node's values if the distance of the path we're
//                //    // currently checking is shorter than the one we found before.
//                //    if (travelledDistance < nodeDict[route.To].Value)
//                //    {
//                //        nodeDict[route.To].Value = travelledDistance;
//                //        nodeDict[route.To].PreviousNode = nodeDict[queue.First.Value.Name];
//                //    }

//                //    // We don't add the 'to' node to the queue if it has already been
//                //    // visited and we don't allow duplicates.
//                //    if (!queue.HasLetter(route.To))
//                //    {
//                //        queue.AddNodeWithPriority(nodeDict[route.To]);
//                //    }
//                //}
//                //unvisited.Remove(queue.First.Value.Name);
//                //queue.RemoveFirst();

//                //CheckNode(queue, destinationNode);
//                // Print out the result
//                PrintShortestPath(startNode, destNode);
//            }



//        }

//        private static void initGraph(int codeKindSite, List<Sites> list)
//        {
//            SiteBL s = new SiteBL();
//            List<Sites> fitSites = s.GetSitesByCodeKindSite(codeKindSite);
//            //choose 20 sites from the list
//            if (fitSites.Count() > 20)
//            {
//                int mis;
//                List<Sites> fitSitesFirst = fitSites;
//                fitSites = new List<Sites> ();
//                Random rand = new Random();
//                for (int i = 0; i < 20; i++)
//                {
//                    mis= rand.Next(0, fitSitesFirst.Count);
//                    fitSites.Add(fitSitesFirst.ElementAt(mis)) ;
//                    fitSitesFirst.RemoveAt(mis);
//                }
//            }
//    foreach (var v in fitSites)
//            {
               
//                var(from, to, distance) = (list.Last().CodeSite, v.CodeSite,/*the distance is taken from google maps*/1);
//                if (!nodeDict.ContainsKey(from)) { nodeDict.Add(from, new Node(from)); }
//if (!nodeDict.ContainsKey(to)) { nodeDict.Add(to, new Node(to)); }
//unvisited.Add(from);
//unvisited.Add(to);

//routes.Add(new Route(from, to, distance));
//            }
//        }

//        private static void PrintOverview()
//{
//    Console.WriteLine("Nodes:");
//    foreach (Node node in nodeDict.Values)
//    {
//        Console.WriteLine("\t" + node.Name);
//    }

//    Console.WriteLine("\nRoutes:");
//    foreach (Route route in routes)
//    {
//        Console.WriteLine("\t" + route.From + " -> " + route.To + " Distance: " + route.Distance);
//    }
//}

//// Get user input for the start and destionation nodes.
//private static (string, string) GetStartAndEnd()
//{
//    Console.WriteLine("\nEnter the start node: ");
//    string startNode = Console.ReadLine();
//    Console.WriteLine("Enter the destination node: ");
//    string destNode = Console.ReadLine();
//    return (startNode, destNode);
//}

//// Called for each node in the graph and iterates over its directly
//// connected nodes. The function always handles the node that
//// currently has the highest priority in our queue.
//// So this function checks any directly connected node and compares
//// the value it currently holds (the shortest path we know to it) is
//// bigger than the distance of the path through the node we're
//// currently checking.
//// If it is, we just found a shorter path to it and we update its
//// 'shortest path' value and also update its previous node to the
//// one we're currently processing.
//// Every directly connected node that we find we also add to the queue
//// (which is sorted by distance), if it's not already in the queue.
//// After we're finished 
//private static void CheckNode(PrioQueue queue, string destinationNode)
//{
//    // If there are no nodes left to check in our queue, we're done.
//    if (queue.Count == 0)
//    {
//        return;
//    }

//    foreach (var route in routes.FindAll(r => r.From == queue.First.Value.Name))
//    {
//        // Skip routes to nodes that have already been visited.
//        if (!unvisited.Contains(route.To))
//        {
//            continue;
//        }

//        double travelledDistance = nodeDict[queue.First.Value.Name].Value + route.Distance;

//        // We only look at nodes we haven't visited yet and we only
//        // update the node's values if the distance of the path we're
//        // currently checking is shorter than the one we found before.
//        if (travelledDistance < nodeDict[route.To].Value)
//        {
//            nodeDict[route.To].Value = travelledDistance;
//            nodeDict[route.To].PreviousNode = nodeDict[queue.First.Value.Name];
//        }

//        // We don't add the 'to' node to the queue if it has already been
//        // visited and we don't allow duplicates.
//        if (!queue.HasLetter(route.To))
//        {
//            queue.AddNodeWithPriority(nodeDict[route.To]);
//        }
//    }
//    unvisited.Remove(queue.First.Value.Name);
//    queue.RemoveFirst();

//    CheckNode(queue, destinationNode);
//}

//// Starts with the destination node and basically adds all the nodes'
//// respective previous nodes to a list, which is then reversed and
//// printed out.
//private static void PrintShortestPath(string startNode, string destNode)
//{
//    //var pathList = new List<String> { destNode };

//    //Node currentNode = nodeDict[destNode];
//    //while (currentNode != nodeDict[startNode])
//    //{
//    //    pathList.Add(currentNode.PreviousNode.Name);
//    //    currentNode = currentNode.PreviousNode;
//    //}

//    //pathList.Reverse();
//    //for (int i = 0; i < pathList.Count; i++)
//    //{
//    //    Console.Write(pathList[i] + (i < pathList.Count - 1 ? " -> " : "\n"));
//    //}
//    //Console.WriteLine("Overall distance: " + nodeDict[destNode].Value);
//}
    }
}

