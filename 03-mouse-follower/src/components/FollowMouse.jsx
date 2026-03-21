import { useState, useEffect } from "react";

function FollowMouse() {
    const [position, setPosition] = useState({ x: 0 , y: 0 });
    const [enabled, setEnabled] = useState(false);

    useEffect(() => {
        console.log('effect', {enabled})

        const handleMove = (event) => {
            const {clientX, clientY} = event;
            setPosition({x: clientX, y: clientY});
        }

        if(enabled){
            window.addEventListener('pointermove', handleMove);
        }

        return () => {
            window.removeEventListener('pointermove', handleMove);
        }
    }, [enabled]);


    return(
        <>
            <div style={{
                position: 'absolute',
                background: '#09f',
                filter: 'blur(10px)',
                borderRadius: '50%',
                opacity: 0.4,
                pointerEvents: 'none',
                left: -25,
                top: -25,
                width: 60,
                height: 60,
                transform: `translate(${position.x}px, ${position.y}px)`
            }}
            />
            <button onClick={() => setEnabled(!enabled)}>
                {enabled ? 'Disactive' : 'Active'} FollowMouse
            </button>
        </>
    )
}

export default FollowMouse;