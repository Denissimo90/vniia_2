import { Participant } from "./Participant";
import { Team } from "./Team";

export class Action {
  actionId: string;
  actionName: string;
  participants: Participant[];
  teams: Team[];
}
