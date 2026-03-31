import { useState, useEffect } from "react";

function FollowMouse() {
    const [position, setPosition] = useState({ x: 0 , y: 0 });
    const [enabled, setEnabled] = useState(false);

    useEffect(() => {
        document.body.classList.toggle('no-cursor', enabled)

        return () => {
            document.body.classList.remove('no-cursor')
        }
    },[enabled])

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
            <div className={enabled ? 'cursor' : 'no-follow-mouse'} style={{
                position: 'absolute',
                background: '#333',
                border: '1px solid #fff',
                borderRadius: '50%',
                opacity: 0.8,
                pointerEvents: 'none',
                left: -25,
                top: -25,
                width: 40,
                height: 40,
                transform: `translate(${position.x}px, ${position.y}px)`
            }}
            />
            <button onClick={() => setEnabled(!enabled)}>
                {enabled ? 'Disable' : 'Enable'} FollowMouse
            </button>
        </>
    )
}

export default FollowMouse;