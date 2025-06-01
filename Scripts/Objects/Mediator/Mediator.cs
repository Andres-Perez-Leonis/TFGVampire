using Godot;
using System;

public partial class Mediator : Node
{
	public void TalkRequest(VillagerTalkingState stateSender, VillagerTalkingState stateRecipiest)
	{

		Villager senderVillager = stateSender.Villager;
		Villager recipiestVillager = stateRecipiest.Villager;


		if (!senderVillager.AvailableToTalk) return;
		if (senderVillager.SuspiciousSystem.iSuspechOf(recipiestVillager)) return;

		
		if (!recipiestVillager.AvailableToTalk || recipiestVillager.SuspiciousSystem.iSuspechOf(senderVillager))
		{
			stateSender.RejectConversation(recipiestVillager);
		}


		

		//sender.Mediator = recipiest.Mediator = this;

		stateSender.InitConversation(recipiestVillager);
		stateRecipiest.InitConversation(senderVillager);
		
	}
}
