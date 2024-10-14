export default class Images {
    public static pathBase: string = "./Heroes";

    static heroes(heroname: string) {
        return {
            icon: require(`${this.pathBase}/Icons/${heroname}.png`),
            minimapIcon: require(`${this.pathBase}/Minimap Icons/${heroname}.png`)
        }
    };
}
