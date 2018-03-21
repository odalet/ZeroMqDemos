using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetMQ;
using NetMQActors.Models;
using Newtonsoft.Json;

namespace NetMQActors
{
    class Program
    {
        static void Main(string[] args)
        {
            /**

            //Round 1 : Should work fine
            EchoShimHandler echoShimHandler = new EchoShimHandler();

            Actor actor = new Actor(NetMQContext.Create(), echoShimHandler, new object[] { "Hello World" });
            actor.SendMore("ECHO");
            string actorMessage = "This is a string";
            actor.Send(actorMessage);
            var result = actor.ReceiveString();
            Console.WriteLine("ROUND1");
            Console.WriteLine("========================");
            string expectedEchoHandlerResult = string.Format("ECHO BACK : {0}", actorMessage);
            Console.WriteLine("ExpectedEchoHandlerResult: '{0}'\r\nGot : '{1}'\r\n",
                expectedEchoHandlerResult, result);
            actor.Dispose();

            //Round 2 : Should NOT work, as we are now using Disposed actor
            try
            {
                Console.WriteLine("ROUND2");
                Console.WriteLine("========================");
                actor.SendMore("ECHO");
                actor.Send("This is a string");
                result = actor.ReceiveString();
            }
            catch (NetMQException nex)
            {
                Console.WriteLine("NetMQException : Actor has been disposed so this is expected\r\n");
            }


            //Round 3 : Should work fine
            echoShimHandler = new EchoShimHandler();

            actor = new Actor(NetMQContext.Create(), echoShimHandler, new object[] { "Hello World" });
            actor.SendMore("ECHO");
            actorMessage = "Another Go";
            actor.Send(actorMessage);
            result = actor.ReceiveString();
            Console.WriteLine("ROUND3");
            Console.WriteLine("========================");
            expectedEchoHandlerResult = string.Format("ECHO BACK : {0}", actorMessage);
            Console.WriteLine("ExpectedEchoHandlerResult: '{0}'\r\nGot : '{1}'\r\n",
                expectedEchoHandlerResult, result);
            actor.Dispose();

            ***/

            //Round 4 : Should work fine
            var accountShimHandler = new AccountShimHandler();

            var accountAction = new AccountAction(TransactionType.Credit, 10m);
            var account = new Account(1, "Test Account", "11223", 0m);

            var accountActor = new Actor(NetMQContext.Create(), accountShimHandler, new[]
            {
                JsonConvert.SerializeObject(accountAction)
            });

            using (accountActor)
            {

                accountActor.SendMore("AMEND ACCOUNT");
                accountActor.Send(JsonConvert.SerializeObject(account));

                var updatedAccount = JsonConvert.DeserializeObject<Account>(accountActor.ReceiveString());

                Console.WriteLine("ROUND4");
                Console.WriteLine("========================");
                var expectedAccountBalance = 10m;
                Console.WriteLine(
                    "Expected Account Balance: '{0}'\r\nGot : '{1}'\r\n" +
                    "Are Same Account Object : '{2}'\r\n",
                    expectedAccountBalance, updatedAccount.Balance,
                    ReferenceEquals(accountActor, updatedAccount));
            }

            Console.ReadLine();
        }
    }
}
