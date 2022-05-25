import { Participant } from "./Participant";

export class Team {
  id: number;
  name: string;
  time: Date = new Date();
  participants: Participant[];
}