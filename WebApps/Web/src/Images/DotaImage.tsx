import React  from 'react';

const DotaImage: React.FC<{ path: string }> = ({path}) => {
    // todo implement normal alt
    return <img src={path} alt={path.split("/").pop()}/>
}

export default DotaImage;
