
import { Competent } from "./Competent";
import { Role } from "./Role";

export class Participant {
  id: number;
  firstName: string;
  secondName: string;
  thirdName: string;
  role: Role;
  competent: Competent;
}