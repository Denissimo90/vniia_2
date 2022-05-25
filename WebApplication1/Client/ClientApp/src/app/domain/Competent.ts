import { GroupWorkplace } from "./GroupWorkplace";
import { Participant } from "./Participant";
import { Team } from "./Team";
import { Workplace } from "./Workplace";

export class Competent {
  id: number;
  title: string;
  shortTitle: string;  
  isGroup: boolean;
  maxGroupSize: number;
  participants: Participant[];
  teams: Team[];
  workplaces: Workplace[];
  groupWorkplaces: GroupWorkplace[];
}
