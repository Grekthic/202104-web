import React, { useEffect, useState } from "react" //  Importar la libreria de React
import Title from "../components/common/title";
import { getTrackById, Track } from "../data/tracks"

export default function Song(): JSX.Element {
    // Variable de estado
    const [track, setTrack] = useState<Track>()

    const loadData = async () => {
        // Cargar la data del track y asignarla a la variable de estado
        const track = await getTrackById(1);
        setTrack(track)
    }

    // En la primera carga de la pagina, llamar al servicio
    // para obtener un track
    useEffect(() => {
        loadData()
    }, []) // Los corchete vacios significa que solo se va a ejecutar este codigo en la primera carga
    return <div>
        {track && <Title text={track.name} />}
        <p>Respuesta en JSON</p>
        {!track && <p>Cargando...</p>}

        <pre>
            {JSON.stringify(track)}
        </pre>
    </div>
}