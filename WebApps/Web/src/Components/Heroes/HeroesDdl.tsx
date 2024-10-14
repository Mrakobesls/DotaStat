import React, {useState} from 'react';

const HeroesDdl: React.FC = () => {
    const [selected, setSelected] = useState('');

    const handleChange = (e: any) => {
        setSelected(e.target.value);
    };

    return (
        <select value={selected} onChange={handleChange}>
            <option value="">Select...</option>
            <option value="option1">Option 1</option>
            <option value="option2">Option 2</option>
            <option value="option3">Option 3</option>
        </select>
    );
}

export default HeroesDdl;
