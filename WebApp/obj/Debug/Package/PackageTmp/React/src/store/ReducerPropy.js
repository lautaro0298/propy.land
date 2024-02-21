
export const PropiedadesReducer = (state, action) => {

    switch (action.type) {
        case "ADD":
            return {
                ...state,
                payload : action.payload
            };
        default:
            return state;

    }
}