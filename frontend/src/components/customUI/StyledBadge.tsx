import Badge from '@material-ui/core/Badge';
import { withStyles, createStyles } from '@material-ui/core/styles';

const StyledBadge = withStyles(() =>
  createStyles({
    badge: {
      right: -20,
      top: 16,
      padding: '0 4px',
    },
  })
)(Badge);

export default StyledBadge;
