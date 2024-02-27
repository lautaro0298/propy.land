const Caracteristica = React.memo(({ caracteristicaId }) => {
    const [nombreCaracteristica, setNombreCaracteristica] = useState("");

    useEffect(() => {
        // Realizar la solicitud de la característica cuando el componente se monte
        const fetchCaracteristica = async () => {
            try {
                const response = await axios.get(`https://propyy.somee.com/api/Caracteristica/obtenerPorID/${caracteristicaId}`);
                if (response.data) {
                    setNombreCaracteristica(response.data.nombreCaracteristica);
                } else {
                    setNombreCaracteristica("Caracteristica no disponible"); // Manejar el caso de error
                }
            } catch (error) {
                console.error("Error al obtener la caracteristica:", error);
                setNombreCaracteristica(""); // Manejar el caso de error
            }
        };

        fetchCaracteristica();
    }, [caracteristicaId]);

    return <span>{nombreCaracteristica}</span>;
});

export default Caracteristica;