import CheckIcon from '@material-ui/icons/Check';
import CloseIcon from '@material-ui/icons/Close'; // Agregar el icono de cierre
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import { sortHotelData } from '../../store/actions';
import CardContent from '@material-ui/core/CardContent';
import styled from 'styled-components'
import axios from "axios";
import { useEffect, useState } from "react";
import { shallowEqual, useDispatch, useSelector } from "react-redux";

const useStyles = makeStyles({
    root: {
        overflow: "auto!important",
        minWidth: "250px",
        maxWidth: "250px",
        maxHeight: "440px",
        minHeight: "200px",
        boxShadow: "1px 1px 20px black",
        margin: "4rem auto",
        position: "absolute",
        top: "-4rem",
        left: "53rem",
        zIndex: "1",
        border: '1px solid lime'
    },
    ratingContent: {
        borderBottom: "1px solid black",
        display: "grid",
        gridTemplateColumns: "2fr 5fr 1fr",
        alignItems: "center",
        fontSize: "22px",
        fontWeight: "bold",
        backgroundColor: "rgb(235,236,237)"
    },
    bullet: {
        display: 'inline-block',
        margin: '0 2px',
        transform: 'scale(0.8)',
    },
    title: {
        fontSize: 14,
    },
    pos: {
        marginBottom: 12,
    },
});

export function RatingCard({ clickedCheckOut, handleClickedCheckOut }) {
    let propiedad = useSelector((state) => state.activities, shallowEqual);
    let propiedadEstado = propiedad.propiedad;
    const [isLoad, setIsLoad] = useState(false);
    const [isOpen, setIsOpen] = useState(true);
    const dispatch = useDispatch();
    const [hotel, setHotel] = useState([]);
    const classes = useStyles();
    const handleClose = () => {
  setIsOpen(false); // actualizar el estado para cerrar la ventana
};
    const [darkgreentick, setDarkgreentick] = React.useState(() => hotel.map((x) => false));
    useEffect(() => {
        
        axios.get("https://propyy.somee.com/api/TipoPropiedad/obtenerTiposPropiedades").then((res) => {

            setHotel(res.data)

        })

    }, []);

    return (
        <RatingWrapper>
            <Card className={classes.root} >
                <CloseIcon onClick={() => {
                    handleClickedCheckOut()
                    console.log('Cerrar ventana');
                }} style={{ position: 'absolute', top: '5px', right: '5px', cursor: 'pointer',color:'red' }} />
                {
                    hotel.map((data, index) => {
                        
                        if (data.tipoPropiedadId == propiedadEstado) {
                            return (
                                <CardContent data-id={index} data-index={index} onClick={(e) => {

                                    const Index = parseInt(e.currentTarget.dataset.index, 10);
                                    const newVisibilities = [...darkgreentick];
                                    newVisibilities[Index] = !newVisibilities[Index];
                                    setDarkgreentick(newVisibilities)
                                    localStorage.removeItem("tipodepropy");
                                    localStorage.setItem('tipodepropy', JSON.stringify(data));
                                    dispatch(sortHotelData(data.tipoPropiedadId))
                                     handleClickedCheckOut()
                                }} className={true ? `greenTicked ${classes.ratingContent}` : `${classes.ratingContent}`}>

                                    <span> {data.nombreTipoPropiedad}</span>
                                    <CheckIcon />
                                </CardContent>); }
                        else {
                            return (
                                <CardContent data-id={index} data-index={index} onClick={(e) => {

                                    const Index = parseInt(e.currentTarget.dataset.index, 10);
                                    const newVisibilities = [...darkgreentick];
                                    newVisibilities[Index] = !newVisibilities[Index];
                                    setDarkgreentick(newVisibilities)
                                    localStorage.removeItem("tipodepropy");
                                    localStorage.setItem('tipodepropy', JSON.stringify(data));
                                    dispatch(sortHotelData(data.tipoPropiedadId))
                                     handleClickedCheckOut()
                                }} className={darkgreentick[index] ? `greenTicked ${classes.ratingContent}` : `${classes.ratingContent}`}>

                                    <span> {data.nombreTipoPropiedad}</span>
                                    <CheckIcon />
                                </CardContent>);
                        }
                    }  )} 
                    
                    
            </Card>
        </RatingWrapper>
    );
}

 





const Grn = styled.p`
width: 25px;
    height: 13px;
    background-color:${(props) => (props.backgroundColor)};
    border-radius: 10px;
    text-align:center;
    span{
  color: white;
    position: relative;
    top: 0px;
    font-weight: bold;
    font-size: 10px;
    }
    
`;

const RatingWrapper = styled.div`
    width: 300px;
    height: 300px;
    margin: auto;
    position: absolute;

    .MuiSvgIcon-root {
    fill: currentColor;
    width: 1em;
    height: 1em;
    display: inline-block;
    font-size: 1.5rem;
    transition: fill 200ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
    flex-shrink: 0;
    user-select: none;
    color: rgb(205,208,210);
}
.greenTicked{
  background-color:white;
    .MuiSvgIcon-root {
    fill: currentColor;
    width: 1em;
    height: 1em;
    display: inline-block;
    font-size: 1.5rem;
    transition: fill 200ms cubic-bezier(0.4, 0, 0.2, 1) 0ms;
    flex-shrink: 0;
    user-select: none;
    color: green;
}
}




`