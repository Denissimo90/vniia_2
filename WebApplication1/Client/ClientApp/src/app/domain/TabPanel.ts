export class TabPanel {
  public static tabMap: Map<number, string> = new Map()
    .set(0, 'Этапы ремонта')

  public static getIdByTitle(title: string) {
    let id = null;
    this.tabMap.forEach((value: string, key: number) => {
      if (value === title) {
        id = key;
      }
    });
    return id;
  }
}
