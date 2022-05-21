import {AuthService} from '@prism/common';

export class User {

  public static ROLE_PREFIX = 'launcher_role_';
  public static PRIVILEGE_PREFIX = 'launcher_privilege_';

  constructor(
    private clientId: string,
    public username: string,
    public name: string,
    public employeeId: string,
    public depnum: string,
    public authorities: Array<any>) {

    const isUser = this.authorities[this.clientId] as Boolean;
    const isRoles = this.authorities[this.clientId]['roles'] as Boolean;

    if (this.authorities[this.clientId] && this.authorities[this.clientId]['roles']) {
      this.roles = this.authorities[this.clientId]['roles'].reduce((result, cur) => {
        result[cur.replace(' ', '_').replace('-', '_')] = true;
        return result;
      }, {});

    } else {
      this.roles = {};
    }
  }

  public roles: any;

  public get shortname(): string {
    return this.name || this.username;
  }
}
