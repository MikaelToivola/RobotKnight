/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGUI : MonoBehaviour {

	private string playerName;
	private int playerLevel;
	private int Health;
	//BattleMouse mouse;


	// Use this for initialization
	void Start () {
		mouse = transform.gameObject.GetComponent<BattleMouse>();

		playerName = GameInformation.PlayerName;
		playerLevel = GameInformation.PlayerLevel;
		//healthbar stuff
	}

	void OnGUI(){
		if (CombatStateMachine.currentState == CombatStateMachine.BattleStates.PLAYERCHOISE) {
			DisplayPlayersChoice ();
		}


	}
	private void DisplayPlayersChoice(){
		
		if (GUI.Button (new Rect (Screen.width - 250, Screen.height - 50, 100, 30), GameInformation.playerAbilityOne.AbilityName)) {
			
			CombatStateMachine.playerUsedAbility = GameInformation.playerAbilityOne;
			
			//CombatStateMachine.currentState = CombatStateMachine.BattleStates.ENEMYCHOICE;
		}

		if (GUI.Button (new Rect (Screen.width - 150, Screen.height - 50, 100, 30), GameInformation.playerAbilityTwo.AbilityName)) {
			/*if (mouse.selectedEnemy != null && mouse.selectedHero != null) {
				float damage = mouse.selectedHero.GetComponent<BattlePlayer> ().intelligence;
				mouse.selectedEnemy.GetComponent<BattlePlayer> ().ReceiveDamage (damage);
			}

				CombatStateMachine.playerUsedAbility = GameInformation.playerAbilityTwo;

		}
		if (GUI.Button (new Rect (Screen.width - 150, Screen.height - 90, 100, 30), "Execute")) {

			if (mouse.selectedEnemy != null && mouse.selectedHero != null) {
				CombatStateMachine.currentState = CombatStateMachine.BattleStates.PLAYERACTION;
			}
		}
	}
}*/
