import { Participant } from "./Participant";
import { Team } from "./Team";

export class Competent {
  id: number;
  title: string;
  shortTitle: string;  
  participants: Participant[];
  teams: Team[];
}
