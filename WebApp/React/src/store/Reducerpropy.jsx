export const PropiedadesReducer = (state, { type, payload}) => {

    switch (type) {
        case "ADD":
            return [...state, payload];
        default:
            return state;

    }
}